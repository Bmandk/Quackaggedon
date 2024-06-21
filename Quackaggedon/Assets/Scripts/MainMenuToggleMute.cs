using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuToggleMute : MonoBehaviour
{
    public Toggle muteToggle;
    public Slider audioSlider;

    private void Awake()
    {
        muteToggle.isOn = false;
        OnValueChanged();
    }

    private void RefreshVolume()
    {
        if (PlayerPrefs.HasKey("SoundMuted"))
        {
            muteToggle.isOn = PlayerPrefs.GetInt("SoundMuted") == 1 ? true : false;

            if (!muteToggle.isOn)
            {
                if (PlayerPrefs.HasKey("Volume"))
                {
                    AudioListener.volume = PlayerPrefs.GetFloat("Volume");
                }
            }
            else
            {
                AudioListener.volume = 0;
            }
        }
    }

    public void OnValueChanged()
    {
        PlayerPrefs.SetInt("SoundMuted", muteToggle.isOn ? 1 : 0);
        RefreshVolume();

        float setVolume = 0;
        if (PlayerPrefs.HasKey("Volume"))
        {
            setVolume = PlayerPrefs.GetFloat("Volume");
        }

        if (!muteToggle.isOn && setVolume == 0)
        {
            AudioListener.volume = 0.6f;
        }
    }
}
