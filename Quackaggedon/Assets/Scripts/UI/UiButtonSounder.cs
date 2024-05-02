using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiButtonSounder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public AudioSource uiSoundAS;
    public AudioClip uiClickSound;
    public AudioClip uiHoverSound;

    public Button button;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiSoundAS.isActiveAndEnabled)// && button.interactable)
        {
            uiSoundAS.clip = uiClickSound;
            uiSoundAS.Play();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiSoundAS.isActiveAndEnabled && button.interactable)
        {
            uiSoundAS.clip = uiHoverSound;
            uiSoundAS.Play();
        }
    }

}

