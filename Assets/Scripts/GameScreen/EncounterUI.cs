using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    private Image EncounterBG, charaImage, enemyImage;

    [SerializeField]
    private SceneInformation sceneInfo;

    private Rect encounterRect;
    private float bg_height, bg_width, curr_height;

    private bool isEncounter = false;
    private bool isNowPlayerturn;

    void Start()
    {
        StartCoroutine(CoUpdate());
    }

    IEnumerator CoUpdate(){
        while(isEncounter)
        {
            if(curr_height <= bg_height)
            {
                curr_height += 150.0f * Time.deltaTime;
                EncounterBG.rectTransform.sizeDelta = new Vector2(bg_width,curr_height);
                yield return new WaitForSeconds(Time.deltaTime);
            } else {
                yield return new WaitForSeconds(2);
                isEncounter = false;
                sceneInfo.mapDictionary = MapManager.Instance.mapDict;
                sceneInfo.isPlayerTurn = isNowPlayerturn;
                SceneManager.LoadScene("Fight Scene");
            }
        }           
    }

    public void doEncounter(bool playerTurn)
    {
        isNowPlayerturn = playerTurn;
        encounterRect = EncounterBG.rectTransform.rect;
        bg_height = encounterRect.height;
        bg_width = encounterRect.width;
        EncounterBG.gameObject.SetActive(true);
        EncounterBG.rectTransform.sizeDelta = new Vector2(bg_width,0);
        charaImage.gameObject.SetActive(false);
        enemyImage.gameObject.SetActive(false);
        isEncounter = true;
    }
}
