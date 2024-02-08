using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textName, textHP;
    [SerializeField]
    private Image HP_bar;
    private Character characterData;

    public void setInformation(Character chara = null){
        if(chara != null){
            characterData = chara;
        }
        textName.text = characterData.charaName;
        textHP.text = characterData.charaHP + "/" + characterData.maxHP;
    }
}
