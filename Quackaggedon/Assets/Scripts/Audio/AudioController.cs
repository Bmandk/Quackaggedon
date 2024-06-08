using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{

    private static AudioController _instance;

    public static AudioController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField]
    private AudioSource _throwSoundAS;

    [SerializeField]
    private AudioSource[] _quackSoundAS;

    [SerializeField]
    private AudioSource _revealSoundAS, _revealBackgroundAS;

    [SerializeField]
    private AudioSource broom, cleanShimmer, TrashSwoosh, TrashSlosh, bloopSound, backgroundMusicAS;

    [SerializeField]
    private AudioSource _equipmentTingAS, revealNewFoodButtonAS;

    [SerializeField]
    private AudioMixerGroup _backgroundMixer, _revealMixer;


    public void DisableBackgroundMusic()
    {
        backgroundMusicAS.gameObject.SetActive(false);
    }

    public void EnableBackgroundMusic() 
    {
        backgroundMusicAS.gameObject.SetActive(true);
    }

    public void PlayCleanShimmer()
    {
        cleanShimmer.Play();
    }
    public void PlayBroom() { broom.Play(); }
    public void PlayTrashSwoosh() { TrashSwoosh.Play(); }
    public void PlayTrashSlosh() { TrashSlosh.Play(); }

    public void PlayBloopSound() { bloopSound.Play(); }

    public void PlayRevealNewFoodButtonSound()
    {
        revealNewFoodButtonAS.Play();
    }

    public void PlayEquipPingSound()
    {
        _equipmentTingAS.Play();
    }

    public void PlayThrowSound()
    {
        _throwSoundAS.pitch = UnityEngine.Random.Range(0.8f, 1.3f);
        _throwSoundAS.Play();
    }

    public void PlayRandomQuack()
    {
        int index = UnityEngine.Random.Range(0, _quackSoundAS.Length);
        _quackSoundAS[index].pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _quackSoundAS[index].Play();
    }


    Coroutine revealSoundC;
    Coroutine backgroundSoundC;
    private string revealVolume = "RevealVolume";
    private string musicVolume = "MusicVolume";
    public void PlayRegularGameSound()
    {
        if (backgroundSoundC != null)
            StopCoroutine(backgroundSoundC);
        if (revealSoundC != null)
            StopCoroutine(revealSoundC);

        backgroundSoundC = StartCoroutine(FadeInSound(0.5f, _backgroundMixer, musicVolume));
        revealSoundC = StartCoroutine(FadeOutSound(0.5f, _revealMixer, revealVolume));
    }

    public void PlayRevealSounds()
    {
        if (backgroundSoundC != null)
            StopCoroutine(backgroundSoundC);
        if (revealSoundC != null)
            StopCoroutine(revealSoundC);

        _revealBackgroundAS.Play();
        _revealSoundAS.Play();

        backgroundSoundC = StartCoroutine(FadeOutSound(0.5f, _backgroundMixer, musicVolume));
        revealSoundC = StartCoroutine(FadeInSound(0.1f, _revealMixer, revealVolume));
    }

    private static IEnumerator FadeOutSound(float duration, AudioMixerGroup mixerGroup, string valueToChange)
    {
        float elapsedTime = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            mixerGroup.audioMixer.SetFloat(valueToChange, Mathf.Lerp(0, -80, t / duration));
            yield return null;
        }

        mixerGroup.audioMixer.SetFloat(valueToChange, -80);
    }

    private static IEnumerator FadeInSound(float duration, AudioMixerGroup mixerGroup, string valueToChange)
    {
        float elapsedTime = 0;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            mixerGroup.audioMixer.SetFloat(valueToChange, Mathf.Lerp(-80, 0, t / duration));
            yield return null;
        }
        mixerGroup.audioMixer.SetFloat(valueToChange, 0);
    }
}
