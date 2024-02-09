using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    private Image EncounterBG, charaImage, enemyImage;

    [SerializeField]
    private SceneInformation sceneInfo;

    [SerializeField]
    private TextMeshProUGUI txtEncounter;

    [SerializeField]
    private List<Sprite> classIcons;

    private Rect encounterRect;
    private float bg_height, bg_width, curr_height;

    private bool isEncounter = false;
    private bool isResult = false;
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
        while(isResult)
        {
            if(curr_height <= bg_height)
            {
                curr_height += 150.0f * Time.deltaTime;
                EncounterBG.rectTransform.sizeDelta = new Vector2(bg_width,curr_height);
                yield return new WaitForSeconds(Time.deltaTime);
            } else {
                yield return new WaitForSeconds(2);
                isResult = false;
                sceneInfo.mapDictionary = null;
                sceneInfo.remainingEnemyMove = null;
                SceneManager.LoadScene("Result Screen");
            }
        }
    }

    public void doEncounter(bool playerTurn)
    {
        isNowPlayerturn = playerTurn;
        encounterRect = EncounterBG.rectTransform.rect;
        bg_height = encounterRect.height;
        bg_width = encounterRect.width;
        txtEncounter.text = "VS";
        EncounterBG.gameObject.SetActive(true);
        EncounterBG.rectTransform.sizeDelta = new Vector2(bg_width,0);
        Encounter encounter = PlayerInventory.encounter[0];
        charaImage.sprite = classIcons[((encounter.ally.charaClass-1)*2)+(encounter.ally.charaType-1)];
        enemyImage.sprite = classIcons[((encounter.enemy.charaClass-1)*2)+(encounter.enemy.charaType-1)];
        enemyImage.rectTransform.sizeDelta = new Vector2((enemyImage.rectTransform.rect.width * (-1)),enemyImage.rectTransform.rect.height);
        isEncounter = true;
    }

    public void doResultScreen(bool isPlayerWin)
    {
        if(isPlayerWin){
            txtEncounter.text = "You win!";
        } else {
            txtEncounter.text = "You lose.";
        }
        sceneInfo.isPlayerWin = isPlayerWin;
        encounterRect = EncounterBG.rectTransform.rect;
        bg_height = encounterRect.height;
        bg_width = encounterRect.width;
        EncounterBG.gameObject.SetActive(true);
        EncounterBG.rectTransform.sizeDelta = new Vector2(bg_width,0);
        charaImage.gameObject.SetActive(false);
        enemyImage.gameObject.SetActive(false);
        isResult = true;
    }
}
