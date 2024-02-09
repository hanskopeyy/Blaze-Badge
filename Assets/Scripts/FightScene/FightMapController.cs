using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FightMapController : MonoBehaviour
{
    [SerializeField]
    private GameObject characterContainer;

    [SerializeField]
    private GameObject charaprefab, infoPrefab, infoContainer;

    [SerializeField]
    private Tilemap charaMap;
    [SerializeField]
    private List<TileData> tileDatas;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private FightController fc;

    private Dictionary<TileBase, TileData> tileDict = new Dictionary<TileBase, TileData>();
    private GameObject allyObject, enemyObject;
    private FightInfo allyInfo, enemyInfo;
    private List<FightInfo> charaInfo = new List<FightInfo>();
    private Character ally, enemy;

    // Start is called before the first frame update
    void Start()
    {
        var encounter = PlayerInventory.encounter.First();
        ally = encounter.ally;
        enemy = encounter.enemy;
        foreach(TileData td in tileDatas){
            foreach(TileBase tb in td.tiles)
            {
                tileDict.Add(tb, td);
            }
        }
        BoundsInt bounds = charaMap.cellBounds;

        for(int y = bounds.min.y; y< bounds.max.y; y++)
        {
            for(int x = bounds.min.x; x < bounds.max.x; x++)
            {
                var tileLoc = new Vector3Int(x,y,0);
                var tileKey = new Vector2Int(x,y);

                // Instantiate Overlay Tile
                if(charaMap.HasTile(tileLoc)){
                    TileData data = tileDict[charaMap.GetTile(tileLoc)];
                    GameObject newCharaInfo = Instantiate(infoPrefab, infoContainer.transform);
                    GameObject newChara = data.isEnemy ? instantiateChara(enemy, newCharaInfo) : instantiateChara(ally, newCharaInfo);
 
                    var cellPosition = charaMap.GetCellCenterWorld(tileLoc);
                    var worldPosition = cam.WorldToScreenPoint(cellPosition);
                    newChara.transform.position = new Vector3(cellPosition.x, cellPosition.y, 2);
                    newCharaInfo.transform.position = new Vector3(worldPosition.x, (worldPosition.y-125), 4);
                    newChara.GetComponent<SpriteRenderer>().sortingOrder = charaMap.GetComponent<TilemapRenderer>().sortingOrder+1;

                    if(data.isEnemy)
                    {
                        enemyObject = newChara;
                        enemyInfo = newCharaInfo.GetComponent<FightInfo>();
                    } else {
                        allyObject = newChara;
                        allyInfo = newCharaInfo.GetComponent<FightInfo>();
                    }
                }
            }
        }
        fc.doFight(allyObject, allyInfo, enemyObject, enemyInfo);
        charaMap.GetComponent<TilemapRenderer>().enabled = false;
    }

    private GameObject instantiateChara(Character chara, GameObject charaInfo)
    {
        GameObject newChara = Instantiate(charaprefab,characterContainer.transform);
        newChara.GetComponent<SetupChara>().setup(chara);
        newChara.transform.localScale = new Vector3(3, 3, 2);

        charaInfo.GetComponent<FightInfo>().setInformation(chara);

        return newChara;
    }
}
