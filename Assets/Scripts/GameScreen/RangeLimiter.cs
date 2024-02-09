using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeLimiter
{
    public List<OverlayTile> GetCharacterRange(OverlayTile charaLoc, int range)
    {
        List<OverlayTile> rangeTiles = new List<OverlayTile>();
        List<OverlayTile> steps = new List<OverlayTile>();
        int stepCount = 0;

        rangeTiles.Add(charaLoc);
        steps.Add(charaLoc);

        while(stepCount < range)
        {
            List<OverlayTile> surround = new List<OverlayTile>();

            foreach(OverlayTile prev in steps)
            {
                surround.AddRange(MapManager.Instance.GetNeighbor(prev, new List<OverlayTile>(), false));
            }

            rangeTiles.AddRange(surround);
            steps = surround.Distinct().ToList();

            stepCount++;
        }

        return rangeTiles.Distinct().ToList();
    }
}
