using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Toggle[] togglesToAffectOnValChange;

    public Image fillSlider;
    public Color unmuteColor;
    public Color muteColor;

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
        if (GetComponent<UnityEngine.UI.Slider>().value > 0)
        {
            foreach (var toggle in togglesToAffectOnValChange)
            {
                toggle.isOn = false;
                SetFillColorUnmute();
            }
        }
        else
        {
            foreach (var toggle in togglesToAffectOnValChange)
            {
                toggle.isOn = true;
                SetFillColorMute();
            }
        }
    }

    public void SetFillColorMute()
    {
        fillSlider.color = muteColor;
    }

    public void SetFillColorUnmute()
    {
        fillSlider.color = unmuteColor;
    }

    public void SetAudioToSmallValue()
    {
        OnValueChanged(0.05f);
        GetComponent<Slider>().value = 0.05f;
    }
}
