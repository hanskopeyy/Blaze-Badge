using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement
{
    public int Score = 0;
    public SetupChara chara;
    public OverlayTile targetTile;
    public bool isEncounter;
    public int moveCost;
    public int distanceToPlayer;

    public EnemyMovement(SetupChara character, OverlayTile tile, bool encounter, int distance)
    {
        chara = character;
        targetTile = tile;
        isEncounter = encounter;
        moveCost = tile.movementCost;
        distanceToPlayer = distance;
        if(isEncounter){
            Score += 16;
        }
        Score += (14-distanceToPlayer);
    }
}
