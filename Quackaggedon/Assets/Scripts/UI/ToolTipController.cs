using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipController : MonoBehaviour
{
    public Canvas parentCanvas;

    public GameObject toolTip;
    public TextMeshProUGUI toolTipText;

    public static string toolTipInfo = "No info";

    public static bool showToolTip = false;

    public void Update()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        toolTip.transform.position = parentCanvas.transform.TransformPoint(movePos);
        toolTipText.text = toolTipInfo;

        if (showToolTip)
        {
            toolTip.SetActive(true);
        }
        else
        {
            toolTip.SetActive(false);
        }

    }
}
