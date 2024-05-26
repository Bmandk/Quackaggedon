using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowUITooltipGeneral : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField]
    private string _toolTipText;
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
