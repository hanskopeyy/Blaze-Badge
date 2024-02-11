using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCharacterManager : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private EnemyListController elc;
    [SerializeField]
    private Button randomEnemyButton;

    private Character selectedChara = null;
    private SelectedCharaUI selectedUI;

    private List<GameObject> characters = new List<GameObject>();    
    private List<Character> lastestLineup;

    public void updateList(List<Character> selectedCharacters)
    {
        lastestLineup = selectedCharacters;
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
        if(selectedChara != null)
        {
            selectedUI.selection();
        }
        selectedChara = selectChara;
        selectedUI = scUI;
        selectedUI.selection();
    }

    public bool equip(Equipment eq)
    {
        if(selectedChara != null && !eq.isEquipped){
            bool isSuccess = selectedChara.equip(eq);
            selectedUI.updateEquipment();
            return isSuccess;
        } else if(eq.isEquipped && eq.equippedTo == selectedChara) {
            bool isSucces = selectedChara.unequip(eq);
            selectedUI.updateEquipment();
            return !isSucces;
        } else {
            return false;
        }
    }

    public void lockLineup(){
        randomEnemyButton.gameObject.SetActive(false);
        foreach(Character c in elc.enemyLineup)
        {
            PlayerInventory.enemyLineUp.Add(new Character(c.charaName, c.charaClass, c.charaType, c.charaHP, c.charaATK, c.charaDEF, c.charaRES));
        }
        foreach(Character c in lastestLineup)
        {
            PlayerInventory.selectedChara.Add(new Character(c.charaName, c.charaClass, c.charaType, c.charaHP, c.charaATK, c.charaDEF, c.charaRES));
        }
        updateList(new List<Character>(PlayerInventory.selectedChara));
        selectEquipTarget(PlayerInventory.selectedChara[0], characters[0].GetComponent<SelectedCharaUI>());

    }
}
