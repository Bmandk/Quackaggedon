using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowFoodTooltip : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public DuckFeeder duckFeeder;
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (DiscoveredObjects.HasAffordedFood(duckFeeder.foodToThrow))
        {
            string toolText = References.Instance.GetFoodData(duckFeeder.foodToThrow).foodTooltipInfo;
            ToolTipController.toolTipInfo = toolText;
        }
        else
        {
            ToolTipController.toolTipInfo = "???";
        }
        ToolTipController.showToolTip = true;
    }
}
