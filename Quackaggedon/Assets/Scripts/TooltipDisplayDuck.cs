using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipDisplayDuck : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public DuckType duckType;

    public void SetToDuck(DuckType duckType)
    {
        this.duckType = duckType;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        ToolTipController.toolTipInfo = References.Instance.GetDuckData(duckType).duckDisplayName;
        ToolTipController.showToolTip = true;
    }
}
