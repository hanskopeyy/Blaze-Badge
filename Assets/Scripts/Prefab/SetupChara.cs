using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupChara : MonoBehaviour
{
    public bool isEnemy;
    public Character characterData;
    public OverlayTile currentPosition;

    public void setup(Character charaData, bool enemy, OverlayTile position)
    {
        if(charaData.charaType == 1){
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255,0,0,1);
        } else {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,255,0,1);
        }
        isEnemy = enemy;
        characterData = charaData;
        currentPosition = position;
    }    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
