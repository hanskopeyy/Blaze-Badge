using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupChara : MonoBehaviour
{
    public bool isEnemy;
    public Character characterData;
    public OverlayTile currentPosition;
    [SerializeField]
    private List<Sprite> classIcons;

    public void setup(Character charaData, bool enemy = false, OverlayTile position = null)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = classIcons[((charaData.charaClass-1)*2)+(charaData.charaType-1)];
        isEnemy = enemy;
        characterData = charaData;
        currentPosition = position;
    }

    public void disableChara()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.25f,0.25f,0.25f,0.5f);
    }

    public void enableChara()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
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
