using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WiggleCoinsIfNotEnough : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GetComponent<Button>().interactable)
        {
            References.Instance.menuController.PlayNotEnoughCoinAnim();
        }
        else
        {
            AudioController.Instance.PlayCoinSound();
        }
    }
}
