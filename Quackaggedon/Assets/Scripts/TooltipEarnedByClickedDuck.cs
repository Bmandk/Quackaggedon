using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipEarnedByClickedDuck : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
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
            ToolTipController.toolTipInfo = $"Total earnings clicking duck: <sprite name=QuacksEmoji_1> {NumberUtility.FormatNumber(PlayerFoodStats.GetTimesDuckClicked(duckType) * References.Instance.GetDuckData(duckType).quacksPerClick)}";
            ToolTipController.showToolTip = true;
        }
        else
        {
            ToolTipController.toolTipInfo = "";
            ToolTipController.showToolTip = false;
        }

    }
}
