using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowTooltipButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
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
        if (GetComponent<Button>().interactable)
        {
            ToolTipController.toolTipInfo = _toolTipText;
            ToolTipController.showToolTip = true;
        }
    }

    private void OnDisable()
    {
        ToolTipController.toolTipInfo = "";
        ToolTipController.showToolTip = false;
    }
}
