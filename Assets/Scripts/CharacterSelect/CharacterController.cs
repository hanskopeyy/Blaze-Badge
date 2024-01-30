using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private Transform selectedScrollViewContent;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private ClickableCharacter clickable;

    private List<Character> availableCharacter;
    private List<Character> selectedChara;
    private List<CharaListUI> charaListUI = new List<CharaListUI>();
    private SelectedCharacterManager scm;

    // Start is called before the first frame update
    void Start()
    {
        availableCharacter = new List<Character>(PlayerInventory.availChara);
        scm = selectedScrollViewContent.GetComponent<SelectedCharacterManager>();
        foreach(Character chara in availableCharacter){
            GameObject newCharaList = Instantiate(prefab, scrollViewContent);
            CharaListUI charaUI = newCharaList.GetComponent<CharaListUI>();
            charaListUI.Add(charaUI);
            charaUI.setCharacter(chara);
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
