using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class ToolTipController : MonoBehaviour
{
    public Canvas parentCanvas;

    public GameObject toolTip;

    public GameObject toolTip_UR;
    public GameObject toolTip_UL;
    public GameObject toolTip_LL;
    public GameObject toolTip_LR;

    public TextMeshProUGUI toolTipText_UR;
    public TextMeshProUGUI toolTipText_UL;
    public TextMeshProUGUI toolTipText_LL;
    public TextMeshProUGUI toolTipText_LR;

    public static string toolTipInfo = "No info";

    public static bool showToolTip = false;

    public void Update()
    {
        UnityEngine.Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        toolTip.transform.position = parentCanvas.transform.TransformPoint(movePos);
        
        toolTipText_UR.text = toolTipInfo;
        toolTipText_UL.text = toolTipInfo;
        toolTipText_LL.text = toolTipInfo;
        toolTipText_LR.text = toolTipInfo;

        if (showToolTip)
        {
            EnableCorrectToolTipSide();
            toolTip.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            toolTip_UL.GetComponent<CanvasGroup>().alpha = 0; 
            toolTip_UR.GetComponent<CanvasGroup>().alpha = 0;
            toolTip_LL.GetComponent<CanvasGroup>().alpha = 0;
            toolTip_LR.GetComponent<CanvasGroup>().alpha = 0;
            toolTip.GetComponent<CanvasGroup>().alpha = 0;
        }
    }



    public void EnableCorrectToolTipSide()
    {
        var position = Input.mousePosition;

        toolTip_UL.GetComponent<CanvasGroup>().alpha = 0;
        toolTip_UR.GetComponent<CanvasGroup>().alpha = 0;
        toolTip_LL.GetComponent<CanvasGroup>().alpha = 0;
        toolTip_LR.GetComponent<CanvasGroup>().alpha = 0;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        bool isLeft = position.x < screenWidth / 2;
        bool isUpper = position.y > screenHeight / 2;

        if (isLeft && isUpper)
        {
            toolTip_LR.GetComponent<CanvasGroup>().alpha = 1;
        }
        else if (isLeft && !isUpper)
        {
            toolTip_UR.GetComponent<CanvasGroup>().alpha = 1;
        }
        else if (!isLeft && isUpper)
        {
            toolTip_LL.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            toolTip_UL.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
