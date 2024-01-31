using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableCharacter : MonoBehaviour
{
    private List<Character> selectedCharacterList = new List<Character>();
    
    [SerializeField]
    private int TeamSize;
    public bool isChanged = false;

    public bool select(Character character){
        if(selectedCharacterList.Contains(character))
        {
            selectedCharacterList.Remove(character);
            isChanged = true;
            return false;
        } else if(selectedCharacterList.Count < TeamSize){
            selectedCharacterList.Add(character);
            isChanged = true;
            return true;
        } else {
            isChanged = false;
            return false;
        }
    }

    public bool isTeamReady(){
        if(selectedCharacterList.Count == TeamSize){
            return true;
        } else {
            return false;
        }
    }

    public List<Character> getSelectedList(){
        return selectedCharacterList;
    }
}
