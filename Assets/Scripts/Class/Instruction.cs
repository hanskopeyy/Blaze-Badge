using UnityEngine;

public class Instruction
{
    public Sprite instructionImage;
    public string instructionHeader;
    public string instructionContent;

    public Instruction(Sprite img, string header, string content)
    {
        instructionImage = img;
        instructionHeader = header;
        instructionContent = content;
    }
}
