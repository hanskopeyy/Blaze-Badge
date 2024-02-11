using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    private int ListState = 0;

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

    public void selectionButton(){
        if(ListState == 0){
            switchToEquipment();
        } else {
            readyToFight();
        }
    }
    public void switchToEquipment(){
        scm.lockLineup();
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
            ListState = 1;
        }
        
    }

    public void readyToFight(){
        SceneManager.LoadScene("Game Screen");
    }

    public bool equipItem(Equipment eqData){
        if(ListState == 1){
            return scm.equip(eqData);
        } else {
            return false;
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
