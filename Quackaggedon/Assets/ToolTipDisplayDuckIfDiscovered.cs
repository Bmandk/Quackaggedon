using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipDisplayDuckIfDiscovered : MonoBehaviour
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
        if (DiscoveredObjects.HasSeenDuck(duckType)) 
        {
            ToolTipController.toolTipInfo = References.Instance.GetDuckData(duckType).duckDisplayName;
        }
        else
        {
            ToolTipController.toolTipInfo = "???";
        }

        ToolTipController.showToolTip = true;
    }
}
