using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headerText, contentText;
    [SerializeField]
    private LayoutElement layout;
    [SerializeField]
    private int wrapLimit;

    public void SetTooltipText(string content, string header = "")
    {
        if(string.IsNullOrEmpty(header))
        {
            headerText.gameObject.SetActive(false);
        } else {
            headerText.gameObject.SetActive(true);
        }
        headerText.text = header;
        contentText.gameObject.SetActive(true);
        contentText.text = content;

        int headerLength = headerText.text.Length;
        int contentLength = contentText.text.Length;

        if(headerLength > wrapLimit || contentLength > wrapLimit)
        {
            layout.enabled = true;
        } else {
            layout.enabled = false;
        }
    }
}
