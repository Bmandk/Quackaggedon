using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static SoundCategorySlider;

public class VolumeSceneLoader : MonoBehaviour
{
    public AudioMixer masterMixer;

    // Start is called before the first frame update
    void Start()
    {
        foreach (SoundCategorySlider.SoundCategory categ in Enum.GetValues(typeof(SoundCategorySlider.SoundCategory)))
        {
            if (PlayerPrefs.HasKey(categ.ToString()))
            {
                float dbVolume = GetVolumeDb(PlayerPrefs.GetFloat(categ.ToString()));
                SetVolume(dbVolume, categ);
            }
        }
    }
    private static float GetVolumeDb(float sliderValue)
    {
        // Convert the slider value (0 to 1) to a logarithmic dB value (-80 to 0)
        return Mathf.Lerp(-80, 0, Mathf.Pow(sliderValue, 0.08f));
    }
    private void SetVolume(float volumeDb, SoundCategorySlider.SoundCategory soundCategory)
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
