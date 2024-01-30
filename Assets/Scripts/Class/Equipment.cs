using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    private int equipId;
    public string equipName;
    public int bonusATK, bonusDEF, bonusRES;
    public bool isEquipped;
    public Character equippedTo;

    public Equipment(string name, int atk, int def, int res){
        equipName = name;
        bonusATK = atk;
        bonusDEF = def;
        bonusRES = res;
        isEquipped = false;
        equippedTo = null;
    }
}
