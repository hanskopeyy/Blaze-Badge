using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class CharaListUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI charaName, charaClass, hp, atk, def, res;
    private Character charaDetails;
    private ClickableCharacter clickable;

    // Start is called before the first frame update
    void Start()
    {
        clickable = this.transform.parent.gameObject.GetComponent<ClickableCharacter>();
    }

    public void setCharacter(Character details)
    {
        charaDetails = details;
        charaName.text = details.charaName;
        switch(details.charaClass){
            case 1: 
                charaClass.text = "Infantry";
                break;
            case 2:
                charaClass.text = "Cavalry";
                break;
            case 3:
                charaClass.text = "Flier";
                break;
        }
        hp.text = details.charaHP.ToString();
        atk.text = details.charaATK.ToString();
        def.text = details.charaDEF.ToString();
        res.text = details.charaRES.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        bool isAdded = clickable.select(charaDetails);
    }

}
