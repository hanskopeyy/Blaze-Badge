using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ResultChecker : MonoBehaviour
{
    [SerializeField]
    private Button backtoMenu;
    [SerializeField]
    private TextMeshProUGUI txt_Result;
    [SerializeField]
    private SceneInformation sceneInfo;
    [SerializeField]
    private Transform scrollViewContent;
    [SerializeField]
    private GameObject rewardPrefab;

    private List<Character> characterList;

    // Start is called before the first frame update
    void Start()
    {
        if(sceneInfo.isPlayerWin)
        {
            txt_Result.text = "You win!";
            calculateResult();
        } else {
            txt_Result.text = "You lose.";
            txt_Result.rectTransform.position = new Vector3(txt_Result.rectTransform.position.x, txt_Result.rectTransform.position.y-100, txt_Result.rectTransform.position.z);
        }

    }
    
    private void calculateResult()
    {
        characterList = new List<Character>(PlayerInventory.selectedChara);
        foreach(Character chara in characterList){
            GameObject newReward = Instantiate(rewardPrefab, scrollViewContent);
        }

    }

    public void goBacktoMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
