using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiate : MonoBehaviour
{
    [SerializeField]
    private SceneInformation sceneInfo;

    void Start()
    {
        if(PlayerInventory.availChara.Count == 0){
            /*
                Unit Class :
                    1 = Invantry
                    2 = Cavalry
                    3 = Flier
                Unit Type:
                    1 = Physical
                    2 = Magic
            PlayerInventory.availChara.Add(new Character(        Name, Class, Type, HP, ATK, DEF, RES )); */
            PlayerInventory.availChara.Add(new Character("Phinfantry",     1,    1, 25,  37,   7,   5 ));
            PlayerInventory.availChara.Add(new Character( "Magfantry",     1,    2, 25,  37,   5,   7 ));
            PlayerInventory.availChara.Add(new Character(  "Phivalry",     2,    1, 30,  28,  12,   9 ));
            PlayerInventory.availChara.Add(new Character( "Magivalry",     2,    2, 30,  28,   9,  12 ));
            PlayerInventory.availChara.Add(new Character(    "Phlier",     3,    1, 20,  37,   5,   5 ));
            PlayerInventory.availChara.Add(new Character(   "Maglier",     3,    2, 20,  37,   5,   5 ));

            /*
            PlayerInventory.ownedEquipment.Add(new Equipment(ID,    Name,+ATK,+DEF,+RES ));*/
            PlayerInventory.ownedEquipment.Add(new Equipment( 1, "Sword",   2,   0,   0 ));
            PlayerInventory.ownedEquipment.Add(new Equipment( 1, "Sword",   2,   0,   0 ));
            PlayerInventory.ownedEquipment.Add(new Equipment( 3, "Armor",   0,   3,   1 ));
            PlayerInventory.ownedEquipment.Add(new Equipment( 2,  "Robe",   0,   1,   3 ));
        }
    }
}
