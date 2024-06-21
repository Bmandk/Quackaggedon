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
    public enum SoundCategory
    {
        Music,
        Effects,
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

    public void OnValueChanged()
    {
        
        // Get the slider value (0 to 1)
        float sliderValue = GetComponent<Slider>().value;

        float volumeDb = GetVolumeDb(sliderValue);

        SetVolume(volumeDb);

        PlayerPrefs.SetFloat(soundCategory.ToString(), sliderValue);
        
    }

    private static float GetVolumeDb(float sliderValue)
    {
        // Convert the slider value (0 to 1) to a logarithmic dB value (-80 to 0)
        return Mathf.Lerp(-80, 0, Mathf.Pow(sliderValue, 0.08f));
    }

    private void SetVolume(float volumeDb)
    {
        switch (soundCategory)
        {
            case SoundCategory.Music:
                masterMixer.SetFloat("MusicVol", volumeDb);
                break;
            case SoundCategory.Effects:
                masterMixer.SetFloat("EffectsVol", volumeDb);
                break;
            default:
                break;
        }
    }
}
