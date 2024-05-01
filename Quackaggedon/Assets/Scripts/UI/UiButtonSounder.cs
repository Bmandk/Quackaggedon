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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiSoundAS.isActiveAndEnabled)
        {
            uiSoundAS.clip = uiClickSound;
            uiSoundAS.Play();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiSoundAS.isActiveAndEnabled)
        {
            uiSoundAS.clip = uiHoverSound;
            uiSoundAS.Play();
        }
    }

}

