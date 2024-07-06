using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static SoundCategorySlider;

public class AudioCategorySetOnLoad : MonoBehaviour
{
    public AudioMixer masterMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey(SoundCategory.MusicSound.ToString()))
        {
            float dbVolume = GetVolumeDb(PlayerPrefs.GetFloat(SoundCategory.MusicSound.ToString()));
            SetVolume(dbVolume, SoundCategory.MusicSound);
        }

        if (PlayerPrefs.HasKey($"SoundMuted{SoundCategory.MusicSound}"))
        {
            var val1 = PlayerPrefs.GetInt($"SoundMuted{SoundCategory.MusicSound}") == 1 ? -80 : 0;
            masterMixer.SetFloat("MusicMuter", val1);
        }

        if (PlayerPrefs.HasKey(SoundCategory.EffectsSound.ToString()))
        {
            float dbVolume = GetVolumeDb(PlayerPrefs.GetFloat(SoundCategory.EffectsSound.ToString()));
            SetVolume(dbVolume, SoundCategory.EffectsSound);
        }

        if (PlayerPrefs.HasKey($"SoundMuted{SoundCategory.EffectsSound}"))
        {
            var val2 = PlayerPrefs.GetInt($"SoundMuted{SoundCategory.EffectsSound}") == 1 ? -80 : 0;
            masterMixer.SetFloat("MusicMuter", val2);
        }
    }

    public static float GetVolumeDb(float sliderValue)
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
