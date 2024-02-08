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
    private int currHP;
    private float maxWidth;
    private bool calcDMG;

    void Awake()
    {
        maxWidth = HP_bar.rectTransform.rect.width;
    }
    public void setInformation(Character chara = null){
        if(chara != null){
            characterData = chara;
        }
        textName.text = characterData.charaName;
        textHP.text = characterData.charaHP + "/" + characterData.maxHP;
        float temp = (float)characterData.charaHP / characterData.maxHP;
        HP_bar.rectTransform.sizeDelta = new Vector2(maxWidth*temp,HP_bar.rectTransform.rect.height);
        currHP = characterData.charaHP;
    }

    public bool calculateInfo(){
        calcDMG = true;
        if(currHP > characterData.charaHP)
        {
            currHP--;
            float temp = (float)currHP / characterData.maxHP;
            HP_bar.rectTransform.sizeDelta = new Vector2(maxWidth*temp,HP_bar.rectTransform.rect.height);
            textHP.text = currHP + "/" + characterData.maxHP;
        } else {
            calcDMG = false;
        }
        return calcDMG;
    }
}
