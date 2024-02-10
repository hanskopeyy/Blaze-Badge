using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    [SerializeField]
    private GameObject scrollViewContent, instructionsPrefab;
    [SerializeField]
    private List<Sprite> images;
    
    void Start()
    {
        string title = "Character Classes";
        List<Instruction> tempInstruction = new List<Instruction>();
        tempInstruction.Add(new Instruction(images[0], "Infantry", "High DMG, Moderate Def"));
        tempInstruction.Add(new Instruction(images[1], "Cavalry", "Moderate DMG, High Def"));
        tempInstruction.Add(new Instruction(images[2], "Flier", "High DMG, Low Def, Can Fly"));

        GameObject instr = Instantiate(instructionsPrefab, scrollViewContent.transform);
        instr.GetComponent<SetupInstructions>().setInstructions(title, tempInstruction);

        title = "Character Type";
        tempInstruction.Clear();
        tempInstruction.Add(new Instruction(images[3], "Physical", "Deal Physical DMG, can be reduced by DEF"));
        tempInstruction.Add(new Instruction(images[4], "Magical", "Deal Magical DMG, can be reduced by RES"));

        instr = Instantiate(instructionsPrefab, scrollViewContent.transform);
        instr.GetComponent<SetupInstructions>().setInstructions(title, tempInstruction);

        title = "Obstacles";
        tempInstruction.Clear();
        tempInstruction.Add(new Instruction(images[5], "Plains", "Traversable for everyone"));
        tempInstruction.Add(new Instruction(images[6], "Forest", "Reduces Infantry movement by 1, impassable for Cavalry, but unhindered for fliers"));
        tempInstruction.Add(new Instruction(images[7], "Mountain", "Traversable by Fliers only"));
        tempInstruction.Add(new Instruction(images[8], "Ruin", "Impassable for all class"));

        instr = Instantiate(instructionsPrefab, scrollViewContent.transform);
        instr.GetComponent<SetupInstructions>().setInstructions(title, tempInstruction);
    }

    public void goBacktoMenu()
    {
        Debug.Log("Clicked!");
        SceneManager.LoadScene("MenuScreen");
    }

}
