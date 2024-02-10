using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupReward : MonoBehaviour
{
    [SerializeField]
    private Image rewardBG, rewardIcon;

    [SerializeField]
    private TextMeshProUGUI txtReward;

    [SerializeField]
    private List<Sprite> bgSprites, rewardIcons;

    public void setReward(int value, Character chara = null)
    {
        if(chara != null){
            rewardBG.sprite = bgSprites[0];
            rewardIcon.sprite = rewardIcons[((chara.charaClass-1)*2)+(chara.charaType-1)];
            rewardIcon.rectTransform.localScale = new Vector3(0.75f, 0.75f, 1);
            txtReward.text = "+" + value +" XP";
        } else {
            rewardIcon.sprite = rewardIcons[6];
            rewardBG.sprite = bgSprites[2];
            txtReward.text = value.ToString();
        }
    }
}
