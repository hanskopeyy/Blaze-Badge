using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTile : MonoBehaviour
{
    public int G,H;
    public int F { get { return G+H;}}

    public int obstacleType;
    public Vector2Int loc;
    public OverlayTile prev;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
