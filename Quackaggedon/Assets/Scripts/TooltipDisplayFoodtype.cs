using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipDisplayFoodtype : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public FoodType foodType;

    public void SetToFood(FoodType foodType)
    {
        this.foodType = foodType;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (DiscoveredObjects.HasSeenFood(foodType)) 
        {
            ToolTipController.toolTipInfo = References.Instance.GetFoodData(foodType).foodTooltipInfo;
        }
        else
        {
            ToolTipController.toolTipInfo = "???";
        }
        ToolTipController.showToolTip = true;
    }
}

