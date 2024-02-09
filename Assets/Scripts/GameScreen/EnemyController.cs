using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<GameObject> enemyObjects, playerObjects;
    private Pathfinder pf;
    private RangeLimiter range;
    private List<EnemyMovement> commitMove = new List<EnemyMovement>();
    private List<OverlayTile> closedList = new List<OverlayTile>();
    private List<OverlayTile> walkPath = new List<OverlayTile>();
    private bool isMoving = false;
    private bool isEncounter = false;
    private TurnController tc;

    [SerializeField]
    private EncounterUI encounterUI;
    [SerializeField]
    private SceneInformation sceneInfo;

    // Start is called before the first frame update
    void Start()
    {
        pf = new Pathfinder();
        range = new RangeLimiter();
    }

    public void startTurn(TurnController turnController)
    {
        if(PlayerInventory.encounter.Count > 0)
        {
            isEncounter = true;
        }
        tc = turnController;
        enemyObjects = MapManager.Instance.getEnemyObjects();
        playerObjects = MapManager.Instance.getPlayerObjects();
        if(sceneInfo.remainingEnemyMove == null)
        {
            Debug.Log("Bot nya lagi mikir");
            foreach(GameObject temp in enemyObjects)
            {
                List<EnemyMovement> possibleMove = new List<EnemyMovement>();
                SetupChara enemy = temp.GetComponent<SetupChara>();
                List<OverlayTile> enemyRange = range.GetCharacterRange(enemy.currentPosition, 3);
                foreach(GameObject pTemp in playerObjects)
                {
                    foreach(OverlayTile tile in enemyRange)
                    {
                        if(closedList.Contains(tile)){
                            continue;
                        }
                        int manhattan = Mathf.Abs(tile.loc.x - pTemp.GetComponent<SetupChara>().currentPosition.loc.x) + Mathf.Abs(tile.loc.y - pTemp.GetComponent<SetupChara>().currentPosition.loc.y);
                        bool isEncounter = false;
                        List<OverlayTile> path = pf.FindPath(enemy.currentPosition, tile, enemyRange, enemy.characterData.charaClass, 3);
                        if(path.Count > 0)
                        {
                            List<OverlayTile> neighbor = MapManager.Instance.GetNeighbor(tile, new List<OverlayTile>());
                            foreach(OverlayTile neighborTile in neighbor)
                            {
                                if(neighborTile.standingChara != null && !neighborTile.isStandingEnemy)
                                {
                                    isEncounter = true;
                                    possibleMove.Add(new EnemyMovement(enemy, tile, isEncounter, manhattan));
                                }
                            }
                            if(!isEncounter){
                                possibleMove.Add(new EnemyMovement(enemy, tile, isEncounter, manhattan));
                            }
                        }
                    }
                }
                EnemyMovement commited = possibleMove.Count > 0 ? possibleMove.OrderBy(x => x.Score).Last() : null;
                if(commited != null){
                    closedList.Add(commited.targetTile);
                    commitMove.Add(commited);
                }
            }
            closedList.Clear();
        } else if(!isEncounter){
            commitMove = sceneInfo.remainingEnemyMove;
            sceneInfo.remainingEnemyMove = null;
            foreach(EnemyMovement eMove in commitMove){
                foreach(GameObject eObject in enemyObjects)
                {
                    if(eObject.GetComponent<SetupChara>().characterData == eMove.chara.characterData){
                        eMove.chara = eObject.GetComponent<SetupChara>();
                        Debug.Log("Replaced New Chara");
                    }
                }
                Debug.Log("Replaced New Tile");
                eMove.targetTile = MapManager.Instance.getTileAt(eMove.targetTile.loc.x,eMove.targetTile.loc.y);
            }
        }
        StartCoroutine(CommitMove());
    }

    IEnumerator CommitMove(){
        while(commitMove.Count > 0 && !isEncounter)
        {
            EnemyMovement move = commitMove[0];
            List<OverlayTile> moveRange = range.GetCharacterRange(move.chara.currentPosition, 3);
            if(isMoving)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                MoveChara(move.chara);
            } else {
                walkPath = pf.FindPath(move.chara.currentPosition, move.targetTile, moveRange, move.chara.characterData.charaClass, 3);
                yield return new WaitForSeconds(1);
                if(walkPath.Count>0)
                {
                    MoveChara(move.chara);
                } else {
                    commitMove.RemoveAt(0);
                }
            }
            if(commitMove.Count == 0){
                tc.changeTurn();
            }
        }
    }

    private void MoveChara(SetupChara character)
    {
        isMoving = true;
        var step = 3 * Time.deltaTime;
        character.transform.position = Vector2.MoveTowards(character.transform.position, walkPath[0].transform.position, step);

        if(Vector2.Distance(character.transform.position, walkPath[0].transform.position) < 0.01f)
        {
            character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, 2);
            character.currentPosition.isBlocked = false;
            character.currentPosition.standingChara = null;
            character.currentPosition.isStandingEnemy = false;
            character.currentPosition = walkPath[0];
            character.currentPosition.isBlocked = true;
            character.currentPosition.standingChara = character.characterData;
            character.currentPosition.isStandingEnemy = true;

            walkPath.RemoveAt(0);
            if(walkPath.Count == 0){
                isMoving = false;
                List<OverlayTile> currentNeighbor = MapManager.Instance.GetNeighbor(character.currentPosition, new List<OverlayTile>());
                foreach(OverlayTile neighbor in currentNeighbor)
                {
                    if(neighbor.standingChara != null && !neighbor.isStandingEnemy){
                        Debug.Log("Enemy Encountered " + neighbor.standingChara.charaName);
                        PlayerInventory.encounter.Add(new Encounter(character.characterData, neighbor.standingChara));
                    }
                }
                commitMove.RemoveAt(0);
                if(PlayerInventory.encounter.Count > 0){
                    isEncounter = true;
                    sceneInfo.remainingEnemyMove = commitMove;
                    encounterUI.doEncounter(false);
                }
            }
        }
    }

}
