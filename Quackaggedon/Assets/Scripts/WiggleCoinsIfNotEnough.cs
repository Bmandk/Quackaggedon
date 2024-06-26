using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WiggleCoinsIfNotEnough : MonoBehaviour, IPointerDownHandler
{
    public Animator buttonAnim;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GetComponent<Button>().interactable)
        {
            buttonAnim.SetTrigger("Invalid");
            References.Instance.menuController.PlayNotEnoughCoinAnim();
        }
        else
        {
            AudioController.Instance.PlayCoinSound();
        }
    }
}
