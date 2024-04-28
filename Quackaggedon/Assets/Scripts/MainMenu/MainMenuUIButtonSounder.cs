using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuUIButtonSounder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public MainMenuUISoundController mainMenuUISoundHandler;
    public void OnPointerClick(PointerEventData eventData)
    {
        mainMenuUISoundHandler.PlayPressUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainMenuUISoundHandler.PlayHoverUI();
    }

}
