using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupInstructions : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtInsTitle;
    [SerializeField]
    private GameObject gridTitle;
    [SerializeField]
    private GameObject instructionsChild;

    public void setInstructions(string Title, List<Instruction> ins)
    {
        txtInsTitle.text = Title;
        foreach(Instruction instruction in ins)
        {
            GameObject childob = Instantiate(instructionsChild, gridTitle.transform);
            childob.GetComponent<SetupInstructionChild>().setInstruct(instruction);
        }
    }
}
