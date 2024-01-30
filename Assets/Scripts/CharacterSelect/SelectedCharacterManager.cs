using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacterManager : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;
    [SerializeField]
    private GameObject prefab;

    private List<GameObject> characters = new List<GameObject>();    

    void Start()
    {
        
    }

    public void updateList(List<Character> selectedCharacters)
    {
        Debug.Log("Updating");
        if(characters.Count > 0){
            foreach(GameObject go in characters){
                Destroy(go);
            }
        }
        foreach(Character chara in selectedCharacters){
            GameObject newChar = Instantiate(prefab,scrollViewContent);
            characters.Add(newChar);
            CharaListUI charaUI = newChar.GetComponent<CharaListUI>();
            charaUI.setCharacter(chara);
        }
    }

    void Update()
    {
        
    }
}
