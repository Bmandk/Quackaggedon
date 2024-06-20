using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMute : MonoBehaviour
{
    public Toggle muteToggle;

    public Toggle[] affectAllTogglesInScene;

    public AudioSlider soundSlider; 
    private void Awake()
    {
        RefreshVolume();
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

        if (muteToggle.isOn) 
        {
            soundSlider.SetFillColorMute();
        }
        else
        {
            soundSlider.SetFillColorUnmute();
        }
    }

    public void OnValueChanged()
    {
        PlayerPrefs.SetInt("SoundMuted", muteToggle.isOn ? 1 : 0);

        foreach (var item in affectAllTogglesInScene)
        {
            item.isOn = muteToggle.isOn;
        }

        RefreshVolume();

        float setVolume = 0;
        if (PlayerPrefs.HasKey("Volume"))
        {
            setVolume = PlayerPrefs.GetFloat("Volume");
        }

        if (!muteToggle.isOn && setVolume == 0)
        {
            soundSlider.SetAudioToSmallValue();
        }
    }
}
