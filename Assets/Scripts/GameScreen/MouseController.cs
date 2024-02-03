using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MouseController : MonoBehaviour
{
    private Pathfinder pf;
    private SetupChara selectedCharacter;

    private List<OverlayTile> path = new List<OverlayTile>();
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;        
        pf = new Pathfinder();
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

            if(Input.GetMouseButtonDown(0))
            {
                var character = collideObject.GetComponent<SetupChara>();
                if(character == null && selectedCharacter != null){
                    Debug.Log("Moving "+ selectedCharacter.characterData.charaName);
                    path = pf.FindPath(selectedCharacter.currentPosition, collideObject.GetComponent<OverlayTile>());
                } else if(!character.isEnemy){
                    Debug.Log(character.characterData.charaName + "is selected");
                    selectedCharacter = character;
                }
            }
        }
        if(path.Count > 0)
        {
            MoveChara();
        }
    }

    private void MoveChara()
    {
        var step = 1 * Time.deltaTime;
        selectedCharacter.transform.position = Vector2.MoveTowards(selectedCharacter.transform.position, path[0].transform.position, step);

        if(Vector2.Distance(selectedCharacter.transform.position, path[0].transform.position) < 0.01f)
        {
            selectedCharacter.currentPosition = path[0];
            path[0].GetComponent<SpriteRenderer>().enabled = false;
            path.RemoveAt(0);
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
