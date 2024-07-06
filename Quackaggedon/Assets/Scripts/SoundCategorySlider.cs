using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SoundCategorySlider : MonoBehaviour
{
    public SoundCategory soundCategory;
    public Toggle toggleToEffect;

    public AudioMixer masterMixer;

    public Color unmuteColor;
    public Color muteColor;

    public Image fill;

    public enum SoundCategory
    {
        MusicSound,
        EffectsSound,
    }
    private void Awake()
    {
        if (PlayerPrefs.HasKey(soundCategory.ToString()))
        {
            GetComponent<UnityEngine.UI.Slider>().value = PlayerPrefs.GetFloat(soundCategory.ToString());
            float dbVolume = GetVolumeDb(PlayerPrefs.GetFloat(soundCategory.ToString()));
            SetVolume(dbVolume);
        }
    }

    public void SetFillColorUnmute()
    {
        fill.color = unmuteColor;
    }

    public void SetFillColorMute()
    {
        fill.color = muteColor; 
    }

    public void SetAudioToSmallValue()
    {
        OnValueChanged(0.05f);
        GetComponent<Slider>().value = 0.05f;
    }

    public void OnValueChanged(float value)
    {
        float volumeDb = GetVolumeDb(value);

        SetVolume(volumeDb);

        PlayerPrefs.SetFloat(soundCategory.ToString(), value);

        if (value == 0)
        {
            toggleToEffect.isOn = true;
        }
        else 
        {
            toggleToEffect.isOn = false;
        }
    }

    public static float GetVolumeDb(float sliderValue)
    {
        // Convert the slider value (0 to 1) to a logarithmic dB value (-80 to 0)
        return Mathf.Lerp(-80, 0, Mathf.Pow(sliderValue, 0.08f));
    }

    private void SetVolume(float volumeDb)
    {
        switch (soundCategory)
        {
            case SoundCategory.MusicSound:
                masterMixer.SetFloat("MusicVol", volumeDb);
                break;
            case SoundCategory.EffectsSound:
                masterMixer.SetFloat("EffectsVol", volumeDb);
                break;
            default:
                break;
        }
    }
}
