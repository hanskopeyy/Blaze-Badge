using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder
{
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end, List<OverlayTile> pathRange, int charaType = -1,  int moveMax = 99)
    {
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        start.movementCost = 0;
        start.remainingMove = moveMax;
        openList.Add(start);

        while (openList.Count > 0)
        {
            OverlayTile curr = openList.OrderBy(x => x.F).First();
            openList.Remove(curr);
            closedList.Add(curr);

            if(curr == end && curr != start)
            {
                return GetFinishList(start, end);                
            }

            List<OverlayTile> neighbor = MapManager.Instance.GetNeighbor(curr, pathRange);
            foreach(OverlayTile n in neighbor)
            {
                if(n.obstacleType == 3 || closedList.Contains(n) || n.isBlocked)
                {
                    continue;
                }
                switch(n.obstacleType){
                    case 0:
                        n.movementCost = curr.movementCost + 1;
                        n.remainingMove = (curr.remainingMove - 1);
                        break;
                    case 1:
                        if(charaType == 3 || charaType == -1)
                        {
                            n.movementCost = curr.movementCost + 1;
                            n.remainingMove = (curr.remainingMove - 1);
                        } else {
                            continue;
                        }
                        break;
                    case 2:
                        if(charaType == 3 || charaType == -1){
                            n.movementCost = 1;
                            n.remainingMove = (curr.remainingMove - 1);
                        } else if (charaType == 2){
                            continue;
                        } else if (charaType == 1){
                            n.movementCost = curr.movementCost + 2;
                            n.remainingMove = (curr.remainingMove - 2);
                        }
                        break;
                }
                if(n.movementCost > moveMax || n.remainingMove < 0){
                    continue;
                }
                n.G = GetManhattan(start,n);
                n.H = GetManhattan(end, n);
                n.prev = curr;
                openList.Add(n);
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
