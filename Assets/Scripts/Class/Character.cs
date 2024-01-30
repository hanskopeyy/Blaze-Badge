using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private int charaId;
    public int charaType;
    /*
        Type :
            1 = Invantry
            2 = Cavalry
            3 = Flier
    */
    public string charaName;
    public int charaATK, charaDEF, charaRES;
    private List<Equipment> currEquip;

    public Character(string name, int type, int atk, int def, int res){
        charaName = name;
        charaType = type;
        charaATK = atk;
        charaDEF = def;
        charaRES = res;
        currEquip = new List<Equipment>();
    }

    public void equip(Equipment e){
        if(currEquip.Count == 0 && !e.isEquipped){
            currEquip.Add(e);
            charaATK += e.bonusATK;
            charaDEF += e.bonusDEF;
            charaRES += e.bonusRES;
            e.isEquipped = true;
            e.equippedTo = this;
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
