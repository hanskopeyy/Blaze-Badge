using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    private static ToolTipManager _instance;
    public static ToolTipManager Instance { get { return _instance; }}
    [SerializeField]
    private TooltipUI tooltip;

    public void Awake()
    {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public static void ShowTooltip(string content, string header = "")
    {
        ToolTipManager.Instance.tooltip.gameObject.SetActive(true);
        ToolTipManager.Instance.tooltip.SetTooltipText(content, header);
    }
    public static void HideTooltip()
    {
        ToolTipManager.Instance.tooltip.gameObject.SetActive(false);
    }

}
