using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder
{
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end, List<OverlayTile> pathRange)
    {
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            OverlayTile curr = openList.OrderBy(x => x.F).First();

            openList.Remove(curr);
            closedList.Add(curr);

            if(curr == end)
            {
                return GetFinishList(start, end);                
            }

            List<OverlayTile> neighbor = MapManager.Instance.GetNeighbor(curr, pathRange);
            foreach(OverlayTile n in neighbor)
            {
                if(n.obstacleType == 3 || closedList.Contains(n))
                {
                    continue;
                }

                n.G = GetManhattan(start,n);
                n.H = GetManhattan(end, n);
                n.prev = curr;
                if(!openList.Contains(n)){
                    openList.Add(n);
                }
            }
        }
        return new List<OverlayTile>();
    }

    private List<OverlayTile> GetFinishList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishList = new List<OverlayTile>();
        OverlayTile current = end;

        while(current != start)
        {
            finishList.Add(current);
            current = current.prev;
        }
        finishList.Reverse();
        return finishList;
    }

    private int GetManhattan(OverlayTile a, OverlayTile b)
    {
        return Mathf.Abs(a.loc.x - b.loc.x) + Mathf.Abs(a.loc.y - b.loc.y);
    }
}
