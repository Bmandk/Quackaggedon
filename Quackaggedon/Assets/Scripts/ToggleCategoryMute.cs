using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static SoundCategorySlider;

public class ToggleCategoryMute : MonoBehaviour
{
    public SoundCategorySlider.SoundCategory soundCategory;

    public Toggle muteToggle;
    public SoundCategorySlider soundSlider;

    public AudioMixer masterMixer;

    private void Awake()
    {
        RefreshVolume();
    }

    private void SetVolume(float volumeDb)
    {
        switch (soundCategory)
        {
            case SoundCategory.MusicSound:
                masterMixer.SetFloat("MusicMuter", volumeDb);
                break;
            case SoundCategory.EffectsSound:
                masterMixer.SetFloat("EffectsMuter", volumeDb);
                break;
            default:
                break;
        }
    }

    private void RefreshVolume()
    {
        if (PlayerPrefs.HasKey($"SoundMuted{soundCategory}"))
        {
            muteToggle.isOn = PlayerPrefs.GetInt($"SoundMuted{soundCategory}") == 1 ? true : false;

            if (!muteToggle.isOn)
            {
                if (PlayerPrefs.HasKey(soundCategory.ToString()))
                {
                    float dbVolume = GetVolumeDb(PlayerPrefs.GetFloat(soundCategory.ToString()));
                    SetVolume(dbVolume);
                }
            }
            else
            {
                SetVolume(-80);
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

    public void OnValueChanged(bool value)
    {
        if (value)
        {
            SaveMuteValue(soundCategory, -80);
        }
        else 
        {
            SaveMuteValue(soundCategory, 0);
        }
        PlayerPrefs.SetInt($"SoundMuted{soundCategory}", value ? 1 : 0);

        RefreshVolume();

        SetSliderToCorrectValue(soundCategory);
    }

    private void SaveMuteValue(SoundCategorySlider.SoundCategory soundCategory, float value)
    {
        switch (soundCategory)
        {
            case SoundCategory.MusicSound:
                PlayerPrefs.SetFloat("MusicVol", value);
                break;
            case SoundCategory.EffectsSound:
                PlayerPrefs.SetFloat("EffectsVol", value);
                break;
            default:
                break;
        }
    }

    private void SetSliderToCorrectValue(SoundCategorySlider.SoundCategory soundCategory)
    {
        if (soundCategory == SoundCategory.EffectsSound) 
        {
            float setEffectsVolume = -80;
            if (PlayerPrefs.HasKey("EffectsVol"))
            {
                setEffectsVolume = PlayerPrefs.GetFloat("EffectsVol");
            }

            if (!muteToggle.isOn && setEffectsVolume == -80)
            {
                soundSlider.SetAudioToSmallValue();
            }
        }
        else if (soundCategory == SoundCategory.MusicSound)
        {
            float setMusicVolume = -80;
            if (PlayerPrefs.HasKey("MusicVol"))
            {
                setMusicVolume = PlayerPrefs.GetFloat("MusicVol");
            }

            if (!muteToggle.isOn && setMusicVolume == -80)
            {
                soundSlider.SetAudioToSmallValue();
            }
        }

    }
}
