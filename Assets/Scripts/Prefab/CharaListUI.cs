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
    [SerializeField]
    private List<Sprite> classIcons;
    [SerializeField]
    private Image charaClassIcon, charaImage;
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
                if(details.charaType == 1)
                {
                    charaClassIcon.sprite = classIcons[0];
                    charaImage.sprite = classIcons[0];
                } else {
                    charaClassIcon.sprite = classIcons[1];
                    charaImage.sprite = classIcons[1];
                }
                break;
            case 2:
                charaClass.text = "Cavalry";
                if(details.charaType == 1)
                {
                    charaClassIcon.sprite = classIcons[2];
                    charaImage.sprite = classIcons[2];
                } else {
                    charaClassIcon.sprite = classIcons[3];
                    charaImage.sprite = classIcons[3];
                }
                break;
            case 3:
                charaClass.text = "Flier";
                if(details.charaType == 1)
                {
                    charaClassIcon.sprite = classIcons[4];
                    charaImage.sprite = classIcons[4];
                } else {
                    charaClassIcon.sprite = classIcons[5];
                    charaImage.sprite = classIcons[5];
                }
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
