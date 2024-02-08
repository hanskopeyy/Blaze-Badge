using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneInformation : ScriptableObject
{
    public bool isFirstLoad = true;
    public Dictionary<Vector2Int,OverlayTile> mapDictionary;
}
