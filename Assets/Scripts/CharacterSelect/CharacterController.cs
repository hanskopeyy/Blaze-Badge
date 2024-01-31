using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private Transform selectedScrollViewContent;

    [SerializeField]
    private GameObject prefab, prefabEq;

    [SerializeField]
    private ClickableCharacter clickable;

    [SerializeField]
    private TextMeshProUGUI buttonText;

    private List<Character> availableCharacter;
    private List<Equipment> equipmentInv;
    private List<Character> selectedChara;
    private List<GameObject> charaList = new List<GameObject>();
    private List<GameObject> equipList = new List<GameObject>();
    private SelectedCharacterManager scm;

    // Start is called before the first frame update
    void Start()
    {
        availableCharacter = new List<Character>(PlayerInventory.availChara);
        equipmentInv = new List<Equipment>(PlayerInventory.ownedEquipment);
        scm = selectedScrollViewContent.GetComponent<SelectedCharacterManager>();
        foreach(Character chara in availableCharacter){
            GameObject newCharaList = Instantiate(prefab, scrollViewContent);
            CharaListUI charaUI = newCharaList.GetComponent<CharaListUI>();
            charaList.Add(newCharaList);
            charaUI.setCharacter(chara);
        }
    }

    public void switchToEquipment(){
        if(clickable.isTeamReady()){
            foreach(GameObject go in charaList){
                Destroy(go);
            }
            charaList.Clear();
            buttonText.text = "Ready!";
            foreach(Equipment eq in equipmentInv)
            {
                GameObject newEq = Instantiate(prefabEq, scrollViewContent);
                EquipmentListUI eqListUI = newEq.GetComponent<EquipmentListUI>();
                equipList.Add(newEq);
                eqListUI.setEquip(eq);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(clickable.isChanged){
            clickable.isChanged = false;
            scm.updateList(clickable.getSelectedList());
        }
    }
}
