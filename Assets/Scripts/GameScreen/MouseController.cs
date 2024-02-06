using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static ArrowShower;
using static ToolTipManager;

public class MouseController : MonoBehaviour
{
    private Pathfinder pf;
    private RangeLimiter range;
    private SetupChara selectedCharacter;

    private List<OverlayTile> path = new List<OverlayTile>();
    private List<OverlayTile> rangeTiles = new List<OverlayTile>();
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;        
        pf = new Pathfinder();
        range = new RangeLimiter();
    }

    // Update is called once per frame
    void Update()
    {
        var focusedTiledHit = GetFocusedOnTile();
        if(focusedTiledHit.HasValue)
        {
            var collideObject = focusedTiledHit.Value.collider.gameObject;
            transform.position = collideObject.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = collideObject.GetComponent<SpriteRenderer>().sortingOrder+5;
            var overlay = collideObject.GetComponent<OverlayTile>();

            if(rangeTiles.Count > 0 && !isMoving && overlay != null){
                path = pf.FindPath(selectedCharacter.currentPosition, overlay, rangeTiles, 3, selectedCharacter.characterData.charaClass);

                foreach(OverlayTile tile in rangeTiles)
                {
                    tile.SetArrow(ArrowDir.None);
                }

                for(int i = 0; i<path.Count; i++)
                {
                    OverlayTile prev = i > 0 ? path[i - 1] : selectedCharacter.currentPosition;
                    OverlayTile next = i < path.Count - 1 ? path[i + 1] : null;
                    
                    path[i].SetArrow(ArrowShower.GetDirection(prev, path[i],next));
                }
            } else if(collideObject.GetComponent<SetupChara>() != null)
            {
                Character characterInfo = collideObject.GetComponent<SetupChara>().characterData;
                string tooltipContent = "";
                switch(characterInfo.charaType){
                    case 1:
                        tooltipContent += "Physical ";
                        break;
                    case 2:
                        tooltipContent += "Magical ";
                        break;
                }
                switch(characterInfo.charaClass){
                    case 1:
                        tooltipContent += "Infantry\n";
                        break;
                    case 2:
                        tooltipContent += "Cavalry\n";
                        break;
                    case 3:
                        tooltipContent += "Flier\n";
                        break;
                }
                tooltipContent += "HP: " + characterInfo.charaHP.ToString() + "\n";
                tooltipContent += "ATK: " + characterInfo.charaATK.ToString() + "\n";
                tooltipContent += "DEF: " + characterInfo.charaDEF.ToString() + "\n";
                tooltipContent += "RES: " + characterInfo.charaRES.ToString();
                ToolTipManager.ShowTooltip(tooltipContent, characterInfo.charaName);
            }

            if(Input.GetMouseButtonDown(0))
            {
                var character = collideObject.GetComponent<SetupChara>();
                if(character == null && selectedCharacter != null){
                    if(path.Count > 0)
                    {
                        MoveChara();
                    }
                } else if(!character.isEnemy && character != null){
                    Debug.Log(character.characterData.charaName + "is selected");
                    selectedCharacter = character;
                    GetRange();
                }
            }
        }
        if(isMoving == true)
        {
            MoveChara();
        }
    }

    private void GetRange()
    {
        foreach(OverlayTile tile in rangeTiles)
        {
            tile.HideTile();
        }
        rangeTiles = range.GetCharacterRange(selectedCharacter.currentPosition, 3);
        foreach(OverlayTile tile in rangeTiles)
        {
            tile.ShowTile();
        }
    }

    private void MoveChara()
    {
        isMoving = true;
        var step = 1 * Time.deltaTime;
        selectedCharacter.transform.position = Vector2.MoveTowards(selectedCharacter.transform.position, path[0].transform.position, step);

        if(Vector2.Distance(selectedCharacter.transform.position, path[0].transform.position) < 0.01f)
        {
            selectedCharacter.currentPosition.isBlocked = false;
            selectedCharacter.currentPosition = path[0];
            selectedCharacter.currentPosition.isBlocked = true;
            path.RemoveAt(0);
            if(path.Count == 0){
                isMoving = false;
                GetRange();
            }
        }
    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if(hits.Length > 0){
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        return null;
    }
}
