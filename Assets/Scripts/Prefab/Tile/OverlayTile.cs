using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArrowShower;

public class OverlayTile : MonoBehaviour
{
    public int G,H;
    public int F { get { return G+H;}}

    public int obstacleType;
    public bool isBlocked;
    public int movementCost;
    public int remainingMove;
    public Character standingChara = null;
    public bool isStandingEnemy;
    public Vector2Int loc;
    public OverlayTile prev;

    [SerializeField]
    public List<Sprite> arrows;
    [SerializeField]
    public SpriteRenderer arrowSR;

    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        SetArrow(ArrowDir.None);
    }
    public void SetArrow(ArrowDir d)
    {
        if(d == ArrowDir.None){
            arrowSR.enabled = false;
        } else {
            arrowSR.enabled = true;
            arrowSR.sprite = arrows[(int)d];
        }
    }
}
