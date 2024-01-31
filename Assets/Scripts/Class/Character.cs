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
    public int charaHP, charaATK, charaDEF, charaRES;
    private List<Equipment> currEquip;

    public Character(string name, int newclass, int type, int hp, int atk, int def, int res){
        charaName = name;
        charaClass = newclass;
        charaType = type;
        charaHP = hp;
        charaATK = atk;
        charaDEF = def;
        charaRES = res;
        currEquip = new List<Equipment>();
    }

    public Equipment getCurrentEquip(){
        if(currEquip.Count > 0){
            return currEquip[0];
        } else {
            return null;
        }
    }

    public void equip(Equipment e){
        if(currEquip.Count == 0 && !e.isEquipped){
            currEquip.Add(e);
            charaATK += e.bonusATK;
            charaDEF += e.bonusDEF;
            charaRES += e.bonusRES;
            e.isEquipped = true;
            e.equippedTo = this;
            Debug.Log(charaName + " equipped with " + e.equipName);
        } else {
            return;
        }
    }

    public void unequip(Equipment e){
        if(currEquip.Contains(e) && e.equippedTo == this){
            currEquip.Remove(e);
            charaATK -= e.bonusATK;
            charaDEF -= e.bonusDEF;
            charaRES -= e.bonusRES;
            e.equippedTo = null;
            e.isEquipped = false;
        } else {
            return;
        }
    }
}
