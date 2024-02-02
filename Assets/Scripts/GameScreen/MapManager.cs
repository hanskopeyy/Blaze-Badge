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
    private GameObject container;
    [SerializeField]
    private Tilemap tileMap, obstacleMap, charaMap;

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
        BoundsInt bounds = tileMap.cellBounds;

        for(int y = bounds.min.y; y< bounds.max.y; y++)
        {
            for(int x = bounds.min.x; x < bounds.max.x; x++)
            {
                var tileLoc = new Vector3Int(x,y,0);

                // Instantiate Overlay Tile
                if(tileMap.HasTile(tileLoc)){
                    var overlayTile = Instantiate(prefab, container.transform);
                    var cellWorldPosition = tileMap.GetCellCenterWorld(tileLoc);

                    overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, 1);
                    overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                }

                // Instantiate Character
                if(charaMap.HasTile(tileLoc))
                {
                    
                }
            }
        }


    }
}
