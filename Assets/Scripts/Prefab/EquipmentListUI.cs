using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class EquipmentListUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI eqName, atk, def, res;
    private Equipment eqData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setEquip(Equipment details)
    {
        eqData = details;
        eqName.text = details.equipName;
        atk.text = "+" + details.bonusATK.ToString();
        def.text = "+" + details.bonusDEF.ToString();
        res.text = "+" + details.bonusRES.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // bool isAdded = clickable.select(charaDetails);
    }
}
