using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetupInstructionChild : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtHeader, txtContent;
    [SerializeField]
    private Image imgInstruction;

    public void setInstruct(Instruction ins)
    {
        txtHeader.text = ins.instructionHeader;
        txtContent.text = ins.instructionContent;
        imgInstruction.sprite = ins.instructionImage;
    }
}
