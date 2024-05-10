using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowFoodTooltip : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TryGetComponent<DuckFeeder>(out DuckFeeder duckFeeder))
        {
            string toolText = References.Instance.GetFoodData(duckFeeder.foodToThrow).foodTooltipInfo;
            ToolTipController.toolTipInfo = toolText;
            ToolTipController.showToolTip = true;
        }
        else
        {
            Debug.LogError("There is no DuckFeeder-script attached to this Gameobject. The ShowFoodToltip class relies on the DuckFeeder to get which tooltip to show. Please add a DuckFeeder or change the way this code works.");
        }
    }
}
