using UnityEngine;

public class ArrowShower
{
    public enum ArrowDir
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        UpLeft = 3,
        UpRight = 4,
        DownLeft = 5,
        DownRight = 6,
        UpEnd = 7,
        DownEnd = 8,
        RightEnd = 9,
        LeftEnd = 10
    }

    public static ArrowDir GetDirection(OverlayTile prev, OverlayTile curr, OverlayTile next)
    {
        bool isEnd = (next == null);
        Vector2Int pastDir, nextDir, direction;

        if(prev != null)
        {
            pastDir = curr.loc - prev.loc;
        } else {
            pastDir = new Vector2Int(0,0);
        }
        if(next != null)
        {
            nextDir = next.loc - curr.loc;
        } else {
            nextDir = new Vector2Int(0,0);
        }
        if(pastDir != nextDir)
        {
            direction = pastDir+nextDir;
        } else {
            direction = nextDir;
        }

        if(!isEnd)
        {
            if( direction == (new Vector2Int(0,1)))
            {
                return ArrowDir.Horizontal;
            }
            if( direction == (new Vector2Int(0,-1)))
            {
                return ArrowDir.Horizontal;
            }
            if( direction == (new Vector2Int(1,0)))
            {
                return ArrowDir.Vertical;
            }
            if( direction == (new Vector2Int(-1,0)))
            {
                return ArrowDir.Vertical;
            }
            if( direction == (new Vector2Int(1,1)))
            {
                if(pastDir.y < nextDir.y) { return ArrowDir.DownLeft; }
                else return ArrowDir.UpRight;
            }
            if( direction == (new Vector2Int(-1,1)))
            {
                if(pastDir.y < nextDir.y) { return ArrowDir.DownRight; }
                else return ArrowDir.UpLeft;
            }
            if( direction == (new Vector2Int(1,-1)))
            {
                if(pastDir.y > nextDir.y) { return ArrowDir.UpLeft; }
                else return ArrowDir.DownRight;
            }
            if( direction == (new Vector2Int(-1,-1)))
            {
                if(pastDir.y > nextDir.y) { return ArrowDir.UpRight; }
                else return ArrowDir.DownLeft;
            }
        } else {
            if( direction == new Vector2Int(0,1))
            {
                return ArrowDir.UpEnd;
            }
            if( direction == new Vector2Int(0,-1))
            {
                return ArrowDir.DownEnd;
            }
            if( direction == new Vector2Int(1,0))
            {
                return ArrowDir.RightEnd;
            }
            if( direction == new Vector2Int(-1,0))
            {
                return ArrowDir.LeftEnd;
            }
        }
        return ArrowDir.None;
    }
}
