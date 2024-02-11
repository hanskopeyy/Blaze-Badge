using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private int charaId;
    public int charaClass;
    /*
        Unit Class :
            1 = Invantry
            2 = Cavalry
            3 = Flier
    */
    public int charaType;
    /*
        Unit Type:
            1 = Physical
            2 = Magic
    */
    public string charaName;
    public int maxHP;
    public int charaHP, charaATK, charaDEF, charaRES;
    public int movementPts;
    private List<Equipment> currEquip;

    public Character(string name, int newclass, int type, int hp, int atk, int def, int res){
        charaName = name;
        charaClass = newclass;
        charaType = type;
        charaHP = hp;
        maxHP = hp;
        charaATK = atk;
        charaDEF = def;
        charaRES = res;
        currEquip = new List<Equipment>();
        movementPts = 3;
    }

    public Equipment getCurrentEquip(){
        if(currEquip.Count > 0){
            return currEquip[0];
        } else {
            return null;
        }
    }

    public bool equip(Equipment e){
        if(currEquip.Count == 0 && !e.isEquipped){
            currEquip.Add(e);
            charaATK += e.bonusATK;
            charaDEF += e.bonusDEF;
            charaRES += e.bonusRES;
            e.isEquipped = true;
            e.equippedTo = this;
            return true;
        } else {
            return false;
        }
    }

    public bool unequip(Equipment e){
        if(currEquip.Contains(e) && e.equippedTo == this){
            currEquip.Remove(e);
            charaATK -= e.bonusATK;
            charaDEF -= e.bonusDEF;
            charaRES -= e.bonusRES;
            e.equippedTo = null;
            e.isEquipped = false;
            return true;
        } else {
            return false;
        }
    }
}
