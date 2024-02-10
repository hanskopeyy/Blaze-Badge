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
    private TextMeshProUGUI textDMG, textDMG_outline;
    [SerializeField]
    public GameObject dmgTextContainer;

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
    public void setDMGText(int dmg)
    {
        textDMG.text = "-" + dmg;
        textDMG_outline.text = "-" + dmg;
        textDMG.gameObject.SetActive(true);
        textDMG_outline.gameObject.SetActive(true);
    }

    public bool calculateInfo(){
        calcDMG = true;
        if(currHP > characterData.charaHP)
        {
            currHP--;
            float temp = (float)currHP / characterData.maxHP;
            dmgTextContainer.transform.position = new Vector3(dmgTextContainer.transform.position.x, dmgTextContainer.transform.position.y+1, dmgTextContainer.transform.position.z);
            textDMG.color = new Color(1,1,1,temp);
            textDMG_outline.color = new Color(1,1,1,temp);
            HP_bar.rectTransform.sizeDelta = new Vector2(maxWidth*temp,HP_bar.rectTransform.rect.height);
            textHP.text = currHP + "/" + characterData.maxHP;
        } else {
            textDMG.color = new Color(1,1,1,0);
            textDMG_outline.color = new Color(1,1,1,0);
            textDMG.gameObject.SetActive(false);
            textDMG_outline.gameObject.SetActive(false);
            calcDMG = false;
        }
        return calcDMG;
    }
}
