using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class EquipmentListUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI eqName, atk, def, res;
    [SerializeField]
    private List<Sprite> equipmentIcons;
    [SerializeField]
    private Image equipmentImage;
    private Equipment eqData;
    private CharacterController charaControl;
    // Start is called before the first frame update
    void Start()
    {
        charaControl = this.transform.parent.gameObject.GetComponent<CharacterController>();
    }

    public void setEquip(Equipment details)
    {
        equipmentImage.sprite = equipmentIcons[details.equipId-1];
        eqData = details;
        eqName.text = details.equipName;
        atk.text = "+" + details.bonusATK.ToString();
        def.text = "+" + details.bonusDEF.ToString();
        res.text = "+" + details.bonusRES.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        charaControl.equipItem(eqData);
    }
}
