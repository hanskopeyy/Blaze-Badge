using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; }}

    [SerializeField]
    private OverlayTile prefab;
    [SerializeField]
    private List<GameObject> charaprefab;
    [SerializeField]
    private GameObject overlayContainer, enemyContainer, teamContainer;
    [SerializeField]
    private Tilemap tileMap, obstacleMap, charaMap;

    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> tileDict = new Dictionary<TileBase, TileData>();

    private List<Character> enemyList, selectedList;
    private List<GameObject> enemyObjects = new List<GameObject>();
    private List<GameObject> charaObjects = new List<GameObject>();

    private void await()
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

                // Instantiate Overlay Tile
                if(tileMap.HasTile(tileLoc)){
                    var overlayTile = Instantiate(prefab, overlayContainer.transform);
                    var cellWorldPosition = tileMap.GetCellCenterWorld(tileLoc);

                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, 1);
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                }

                // Instantiate Character
                if(charaMap.HasTile(tileLoc))
                {
                    TileData data = tileDict[charaMap.GetTile(tileLoc)];
                    if(data.isEnemy)
                    {
                        GameObject newEnemy = Instantiate(charaprefab[(enemyList[data.num].charaClass)-1],enemyContainer.transform);
                        var cellPosition = tileMap.GetCellCenterWorld(tileLoc);

                        newEnemy.transform.position = new Vector3(cellPosition.x, cellPosition.y, 1);
                        newEnemy.GetComponent<SpriteRenderer>().sortingOrder = charaMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                        newEnemy.GetComponent<SetupChara>().setup(enemyList[data.num], true);
                        enemyObjects.Add(newEnemy);
                    } else {
                        GameObject newTeam = Instantiate(charaprefab[(selectedList[data.num].charaClass)-1],enemyContainer.transform);
                        var cellPosition = tileMap.GetCellCenterWorld(tileLoc);

                        newTeam.transform.position = new Vector3(cellPosition.x, cellPosition.y, 1);
                        newTeam.GetComponent<SpriteRenderer>().sortingOrder = charaMap.GetComponent<TilemapRenderer>().sortingOrder+1;
                        newTeam.GetComponent<SetupChara>().setup(selectedList[data.num], false);
                        charaObjects.Add(newTeam);
                    }
                }
            }
        }
        charaMap.GetComponent<TilemapRenderer>().enabled = false;


    }
}
