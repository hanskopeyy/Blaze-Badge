using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnController : MonoBehaviour
{
    [SerializeField]
    private Button btn_endTurn;
    [SerializeField]
    private TextMeshProUGUI txt_turnStatus;
    
    [SerializeField]
    private EnemyController enemyController;
    [SerializeField]
    private MouseController mc;
    [SerializeField]
    private SceneInformation sceneInfo;

    public void changeTurn()
    {
        mc.HideRangeTiles();
        sceneInfo.isPlayerTurn = sceneInfo.isPlayerTurn ? false : true;
        if(!sceneInfo.isPlayerTurn)
        {
            btn_endTurn.gameObject.SetActive(false);
            txt_turnStatus.text = "Enemy Turn";
            List<Character> enemyTemp = PlayerInventory.enemyLineUp;
            enemyController.startTurn(this);
        } else {
            btn_endTurn.gameObject.SetActive(true);
            txt_turnStatus.text = "Your Turn";
            List<GameObject> playerTemp = MapManager.Instance.getPlayerObjects();
            foreach(GameObject chara in playerTemp)
            {
                chara.GetComponent<SetupChara>().characterData.movementPts = 3;
                chara.GetComponent<SetupChara>().enableChara();
            }
        }
    }

    void Start()
    {
        if(!sceneInfo.isPlayerTurn)
        {
            Debug.Log("Started with Turn lawan");
            btn_endTurn.gameObject.SetActive(false);
            txt_turnStatus.text = "Enemy Turn";
            enemyController.startTurn(this);
        } else {
            btn_endTurn.gameObject.SetActive(true);
            txt_turnStatus.text = "Your Turn";
            List<GameObject> playerTemp = MapManager.Instance.getPlayerObjects();
            foreach(GameObject chara in playerTemp)
            {
                if(chara.GetComponent<SetupChara>().characterData.movementPts > 0)
                {
                    chara.GetComponent<SetupChara>().enableChara();
                } else {
                    chara.GetComponent<SetupChara>().disableChara();
                }
            }
        }
    }
}
