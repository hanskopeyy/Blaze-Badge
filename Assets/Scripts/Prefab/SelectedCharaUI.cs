using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class SelectedCharaUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image img_equip, img_chara;
    SelectedCharacterManager scm;
    Character charaDetails;
    Equipment eqData;
    // Start is called before the first frame update
    void Start()
    {
        scm = this.transform.parent.gameObject.GetComponent<SelectedCharacterManager>();        
        img_equip.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCharacter(Character chara)
    {
        charaDetails = chara;
    }

    public void updateEquipment(){
        eqData = charaDetails.getCurrentEquip();
        if(eqData != null){
            img_equip.enabled = true;
        } else {
            img_equip.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        scm.selectEquipTarget(charaDetails, this);
    }
}
