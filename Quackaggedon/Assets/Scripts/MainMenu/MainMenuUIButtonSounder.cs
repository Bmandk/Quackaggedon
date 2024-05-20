using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUIButtonSounder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public MainMenuUISoundController mainMenuUISoundHandler;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable) 
            mainMenuUISoundHandler.PlayPressUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
            mainMenuUISoundHandler.PlayHoverUI();
    }

}
