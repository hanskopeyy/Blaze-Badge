using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacterManager : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;
    [SerializeField]
    private GameObject prefab;

    private Character selectedChara = null;
    private SelectedCharaUI selectedUI;

    private List<GameObject> characters = new List<GameObject>();    
    public void updateList(List<Character> selectedCharacters)
    {
        if(characters.Count > 0){
            foreach(GameObject go in characters){
                Destroy(go);
            }
        }
        characters.Clear();
        foreach(Character chara in selectedCharacters){
            GameObject newChar = Instantiate(prefab,scrollViewContent);
            SelectedCharaUI scUI = newChar.GetComponent<SelectedCharaUI>();
            scUI.setCharacter(chara);
            characters.Add(newChar);
        }
    }
    public void selectEquipTarget(Character selectChara, SelectedCharaUI scUI){
        selectedChara = selectChara;
        Debug.Log(selectedChara.charaName + " is selected");
        selectedUI = scUI;
    }

    public void equip(Equipment eq)
    {
        if(selectedChara != null && !eq.isEquipped){
            Debug.Log("Trying to equip " + selectedChara.charaName + " with " + eq.equipName);
            selectedChara.equip(eq);
            selectedUI.updateEquipment();
        } else if(eq.isEquipped && eq.equippedTo == selectedChara) {
            selectedChara.unequip(eq);
            selectedUI.updateEquipment();
        }
    }
}
