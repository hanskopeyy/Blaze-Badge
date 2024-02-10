using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public int equipId;
    public string equipName;
    public int bonusATK, bonusDEF, bonusRES;
    public bool isEquipped;
    public Character equippedTo;

    public Equipment(int id, string name, int atk, int def, int res){
        equipId = id;
        equipName = name;
        bonusATK = atk;
        bonusDEF = def;
        bonusRES = res;
        isEquipped = false;
        equippedTo = null;
    }
}
