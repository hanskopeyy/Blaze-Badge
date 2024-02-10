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
    private Image img_equip, img_chara, img_selected;
    [SerializeField]
    private List<Sprite> classIcons, equipmentIcons;
    SelectedCharacterManager scm;
    Character charaDetails;
    Equipment eqData;
    private bool isSelected;
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
        img_chara.sprite = classIcons[((chara.charaClass-1)*2)+(chara.charaType-1)];
        isSelected = true;
        selection();
    }

    public void updateEquipment(){
        eqData = charaDetails.getCurrentEquip();
        if(eqData != null){
            img_equip.sprite = equipmentIcons[eqData.equipId-1];
            img_equip.enabled = true;
        } else {
            img_equip.enabled = false;
        }
    }

    public void selection()
    {
        isSelected = isSelected ? false : true;
        img_selected.gameObject.SetActive(isSelected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        scm.selectEquipTarget(charaDetails, this);
    }
}
