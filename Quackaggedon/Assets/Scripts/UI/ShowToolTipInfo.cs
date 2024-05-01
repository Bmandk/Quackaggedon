using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowToolTipInfo : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public string toolTipInfo;

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipController.toolTipInfo = toolTipInfo;
        ToolTipController.showToolTip = true;
    }
}
