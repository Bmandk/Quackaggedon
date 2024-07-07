using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowUITooltipMuchText : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [TextArea(15, 20)]
    public string _toolTipText;
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        ToolTipController.toolTipInfo = _toolTipText;
        ToolTipController.showToolTip = true;
    }
}

