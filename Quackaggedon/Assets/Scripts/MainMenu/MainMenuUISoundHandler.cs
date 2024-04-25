using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUISoundHandler : MonoBehaviour
{
    public AudioSource uiButtonHoverAS;
    public AudioSource uiButtonPressedAS;

    public void PlayPressUI()
    {
        uiButtonPressedAS.Play();
    }

    public void PlayHoverUI()
    {
        uiButtonHoverAS.Play();
    }
}
