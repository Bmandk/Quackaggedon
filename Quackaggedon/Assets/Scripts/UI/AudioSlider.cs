using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSlider : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            GetComponent<UnityEngine.UI.Slider>().value = PlayerPrefs.GetFloat("Volume");
        }
    }

    public void OnValueChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }
}
