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
    private List<OverlayTile> DetectedList = new List<OverlayTile>();
    private bool isMoving;

    [SerializeField]
    private TurnController tc;
    [SerializeField]
    private EncounterUI encounterUI;

    [SerializeField]
    private SceneInformation sceneInfo;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;        
        pf = new Pathfinder();
        range = new RangeLimiter();
        ToolTipManager.HideTooltip();
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

            if((rangeTiles.Count > 0 && !isMoving && overlay != null) && sceneInfo.isPlayerTurn){
                ToolTipManager.HideTooltip();
                path = pf.FindPath(selectedCharacter.currentPosition, overlay, rangeTiles, selectedCharacter.characterData.charaClass, selectedCharacter.characterData.movementPts);

                foreach(OverlayTile dtile in DetectedList)
                {
                    dtile.HideTile();
                    dtile.GetComponent<SpriteRenderer>().color = new Color(255,255,255,1);
                }
                DetectedList.Clear();
                if(path.Count > 0){
                    List<OverlayTile> finalNeighbor = MapManager.Instance.GetNeighbor(path[( path.Count - 1)], new List<OverlayTile>());
                    foreach(OverlayTile neighbor in finalNeighbor)
                    {
                        if(neighbor.standingChara != null && neighbor.isStandingEnemy){
                            neighbor.GetComponent<SpriteRenderer>().color = new Color(255,0,0,1);
                            neighbor.ShowTile();
                            DetectedList.Add(neighbor);
                        }
                    }
                }

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
            } else {
                var character = collideObject.GetComponent<SetupChara>();
                if(character != null){
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
                } else {
                    ToolTipManager.HideTooltip();
                }
            }

            if(Input.GetMouseButtonDown(0) && sceneInfo.isPlayerTurn)
            {
                var character = collideObject.GetComponent<SetupChara>();
                if(character == null && selectedCharacter != null){
                    if(path.Count > 0)
                    {
                        MoveChara();
                    }
                } else if((!character.isEnemy && character != null) && character.characterData.movementPts > 0){
                    Debug.Log(character.characterData.charaName + "is selected");
                    selectedCharacter = character;
                    GetRange(true);
                }
            }
        }
        if(isMoving == true)
        {
            MoveChara();
        }
    }
    public void HideRangeTiles()
    {
        foreach(OverlayTile tile in rangeTiles)
        {
            tile.HideTile();
        }
        rangeTiles.Clear();
    }

    private void GetRange(bool firstSelect)
    {
        HideRangeTiles();
        if(!firstSelect)
        {
            List<OverlayTile> currentNeighbor = MapManager.Instance.GetNeighbor(selectedCharacter.currentPosition, new List<OverlayTile>());
            foreach(OverlayTile neighbor in currentNeighbor)
            {
                if(neighbor.standingChara != null && neighbor.isStandingEnemy){
                    Debug.Log("Encountered " + neighbor.standingChara.charaName);
                    PlayerInventory.encounter.Add(new Encounter(selectedCharacter.characterData, neighbor.standingChara));
                }
            }
        }
        if(PlayerInventory.encounter.Count > 0){
            encounterUI.doEncounter(true);
        } else if(selectedCharacter.characterData.movementPts > 0){
            rangeTiles = range.GetCharacterRange(selectedCharacter.currentPosition, selectedCharacter.characterData.movementPts);
            foreach(OverlayTile tile in rangeTiles)
            {
                tile.ShowTile();
            }
        } else if(selectedCharacter.characterData.movementPts <= 0){
            selectedCharacter.disableChara();
        }
    }

    private void MoveChara()
    {
        isMoving = true;
        var step = 3 * Time.deltaTime;
        selectedCharacter.transform.position = Vector2.MoveTowards(selectedCharacter.transform.position, path[0].transform.position, step);

        if(Vector2.Distance(selectedCharacter.transform.position, path[0].transform.position) < 0.01f)
        {
            selectedCharacter.transform.position = new Vector3(selectedCharacter.transform.position.x, selectedCharacter.transform.position.y, 2);
            selectedCharacter.currentPosition.isBlocked = false;
            selectedCharacter.currentPosition.standingChara = null;
            selectedCharacter.currentPosition = path[0];
            selectedCharacter.currentPosition.isBlocked = true;
            selectedCharacter.currentPosition.standingChara = selectedCharacter.characterData;
            selectedCharacter.characterData.movementPts = path[0].remainingMove;

            path.RemoveAt(0);
            if(path.Count == 0){
                isMoving = false;
                GetRange(false);
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
