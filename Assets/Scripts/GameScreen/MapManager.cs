using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; }}

    [SerializeField]
    private SceneInformation sceneInfo;

    [SerializeField]
    private OverlayTile prefab;
    [SerializeField]
    private List<GameObject> charaprefab;
    [SerializeField]
    private GameObject overlayContainer, enemyContainer, teamContainer;
    [SerializeField]
    private Tilemap tileMap, obstacleMap, charaMap;
    [SerializeField]
    private EncounterUI encounterUI;

    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> tileDict = new Dictionary<TileBase, TileData>();
    public Dictionary<Vector2Int, OverlayTile> mapDict;
    public Dictionary<Vector2Int, OverlayTile> oldDict;
    private int enemyCounter = 0, allyCounter = 0;

    private List<Character> enemyList, selectedList;
    private List<GameObject> enemyObjects = new List<GameObject>();
    private List<GameObject> charaObjects = new List<GameObject>();
    private List<EnemyMovement> remainingEnemyMovement;

    private void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mapDict = new Dictionary<Vector2Int, OverlayTile>();
        if(!sceneInfo.isFirstLoad)
        {
            oldDict = sceneInfo.mapDictionary;
        } else {
            sceneInfo.isPlayerTurn = true;
            oldDict = null;
        }
        //Initiate Tile Data
        foreach(TileData td in tileDatas){
            foreach(TileBase tb in td.tiles)
            {
                tileDict.Add(tb, td);
            }
        }
        //Initiate Charas
        selectedList = new List<Character>(PlayerInventory.selectedChara);
        enemyList = new List<Character>(PlayerInventory.enemyLineUp);

        BoundsInt bounds = tileMap.cellBounds;

        for(int y = bounds.min.y; y< bounds.max.y; y++)
        {
            for(int x = bounds.min.x; x < bounds.max.x; x++)
            {
                var tileLoc = new Vector3Int(x,y,0);
                var tileKey = new Vector2Int(x,y);

                // Instantiate Overlay Tile
                if(tileMap.HasTile(tileLoc)){
                    var overlayTile = Instantiate(prefab, overlayContainer.transform);
                    var cellWorldPosition = tileMap.GetCellCenterWorld(tileLoc);

                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, 1);
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                    overlayTile.loc = tileKey;

                    // Initiate Obstacles
                    if(obstacleMap.HasTile(tileLoc))
                    {
                        TileData data = tileDict[obstacleMap.GetTile(tileLoc)];
                        overlayTile.obstacleType = data.num;
                    } else {
                        overlayTile.obstacleType = 0;
                    }

                    // Instantiate Character
                    if(charaMap.HasTile(tileLoc) && sceneInfo.isFirstLoad)
                    {
                        TileData data = tileDict[charaMap.GetTile(tileLoc)];
                        if(data.isEnemy)
                        {
                            if(enemyList[data.num].charaHP > 0){
                                GameObject newEnemy = Instantiate(charaprefab[(enemyList[data.num].charaClass)-1],enemyContainer.transform);
                                var cellPosition = tileMap.GetCellCenterWorld(tileLoc);

                                newEnemy.transform.position = new Vector3(cellPosition.x, cellPosition.y, 2);
                                newEnemy.GetComponent<SpriteRenderer>().sortingOrder = charaMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                                newEnemy.GetComponent<SetupChara>().setup(enemyList[data.num], true, overlayTile);
                                overlayTile.isStandingEnemy = true;
                                overlayTile.standingChara = enemyList[data.num];
                                overlayTile.isBlocked = true;
                                enemyObjects.Add(newEnemy);
                                enemyCounter++;
                            }
                        } else {
                            if(selectedList[data.num].charaHP > 0){
                                GameObject newTeam = Instantiate(charaprefab[(selectedList[data.num].charaClass)-1],teamContainer.transform);
                                var cellPosition = tileMap.GetCellCenterWorld(tileLoc);

                                newTeam.transform.position = new Vector3(cellPosition.x, cellPosition.y, 2);
                                newTeam.GetComponent<SpriteRenderer>().sortingOrder = charaMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                                newTeam.GetComponent<SetupChara>().setup(selectedList[data.num], false, overlayTile);
                                overlayTile.isStandingEnemy = false;
                                overlayTile.isBlocked = true;
                                overlayTile.standingChara = selectedList[data.num];
                                charaObjects.Add(newTeam);
                                allyCounter++;
                            }
                        }
                    }
                    if(!sceneInfo.isFirstLoad)
                    {
                        if(oldDict[tileKey].standingChara != null)
                        {
                            Character theChara = oldDict[tileKey].standingChara;
                            if(enemyList.Contains(oldDict[tileKey].standingChara))
                            {
                                if(theChara.charaHP > 0)
                                {
                                    GameObject newEnemy = Instantiate(charaprefab[(theChara.charaClass)-1],enemyContainer.transform);
                                    var cellPosition = tileMap.GetCellCenterWorld(tileLoc);

                                    newEnemy.transform.position = new Vector3(cellPosition.x, cellPosition.y, 2);
                                    newEnemy.GetComponent<SpriteRenderer>().sortingOrder = charaMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                                    newEnemy.GetComponent<SetupChara>().setup(theChara, true, overlayTile);
                                    overlayTile.isStandingEnemy = true;
                                    overlayTile.standingChara = theChara;
                                    overlayTile.isBlocked = true;
                                    enemyObjects.Add(newEnemy);
                                    enemyCounter++;
                                }
                            } else {
                                if(theChara.charaHP > 0)
                                {
                                    GameObject newTeam = Instantiate(charaprefab[(theChara.charaClass)-1],teamContainer.transform);
                                    var cellPosition = tileMap.GetCellCenterWorld(tileLoc);

                                    newTeam.transform.position = new Vector3(cellPosition.x, cellPosition.y, 2);
                                    newTeam.GetComponent<SpriteRenderer>().sortingOrder = charaMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                                    newTeam.GetComponent<SetupChara>().setup(theChara, false, overlayTile);
                                    overlayTile.isStandingEnemy = false;
                                    overlayTile.isBlocked = true;
                                    overlayTile.standingChara = theChara;
                                    charaObjects.Add(newTeam);
                                    allyCounter++;
                                }
                            }
                        }
                    }
                    mapDict.Add(tileKey, overlayTile);
                }
            }
        }
        if(PlayerInventory.encounter.Count > 0){
            remainingEnemyMovement = sceneInfo.remainingEnemyMove;
            encounterUI.doEncounter(sceneInfo.isPlayerTurn);
        } else if(enemyCounter == 0) {
            encounterUI.doResultScreen(true);
        } else if(allyCounter == 0){
            encounterUI.doResultScreen(false);
        }
        charaMap.GetComponent<TilemapRenderer>().enabled = false;
    }

    public List<OverlayTile> GetNeighbor(OverlayTile current, List<OverlayTile> rangeTiles, bool ignoreBlock = true)
    {
        Dictionary<Vector2Int, OverlayTile> tileRange = new Dictionary<Vector2Int, OverlayTile>();
        if(rangeTiles.Count > 0)
        {
            foreach(OverlayTile tile in rangeTiles)
            {
                tileRange.Add(tile.loc, tile);
            }
        } else {
            tileRange = MapManager.Instance.mapDict;
        }

        List<OverlayTile> neighbors = new List<OverlayTile>();
        List<Vector2Int> checkLocations = new List<Vector2Int>();

        checkLocations.Add(new Vector2Int(current.loc.x +1, current.loc.y));
        checkLocations.Add(new Vector2Int(current.loc.x -1, current.loc.y));
        checkLocations.Add(new Vector2Int(current.loc.x, current.loc.y +1));
        checkLocations.Add(new Vector2Int(current.loc.x, current.loc.y -1));

        foreach(Vector2Int check in checkLocations)
        {
            if(ignoreBlock){
                if(tileRange.ContainsKey(check))
                {
                    neighbors.Add(tileRange[check]);
                }
            } else {
                if(tileRange.ContainsKey(check) && !mapDict[check].isBlocked)
                {
                    neighbors.Add(tileRange[check]);
                }
            }
        }
        return neighbors;
    }

    public OverlayTile getTileAt(int x, int y){
        var tileLoc = new Vector2Int(x,y);
        return mapDict[tileLoc];
    }

    public List<GameObject> getEnemyObjects()
    {
        return enemyObjects;
    }

    public List<GameObject> getPlayerObjects()
    {
        return charaObjects;
    }
}
