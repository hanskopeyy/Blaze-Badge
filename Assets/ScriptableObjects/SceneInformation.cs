using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneInformation : ScriptableObject
{
    public bool isFirstLoad = true;
    public bool isPlayerTurn;
    public bool isPlayerWin;
    public List<EnemyMovement> remainingEnemyMove;
    public Dictionary<Vector2Int,OverlayTile> mapDictionary;

    void Awake(){
        isFirstLoad = true;
    }
}
