using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowFoodRemainingTooltip : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField]
    private DuckFeeder duckFeeder;

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var foodCost = duckFeeder.DuckFeederStats.foodCost;
        var nextDuckCost = duckFeeder.NextDuckCost;
        var foodThrown = duckFeeder.FoodThrown;
        var costOfRemaingFood = foodCost * (nextDuckCost - foodThrown);
        ToolTipController.toolTipInfo = $"Food required for next duck. \nAggregate Quack cost: {NumberUtility.FormatNumber(costOfRemaingFood)}";
        ToolTipController.showToolTip = true;
    }
}
