using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuUISoundController : MonoBehaviour
{
    private static MainMenuUISoundController _instance;

    public static MainMenuUISoundController Instance { get { return _instance; } }


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

    public AudioSource uiButtonHoverAS;
    public AudioSource uiButtonPressedAS;

    public AudioSource quack1AS;
    public AudioSource quack2AS;
    public AudioSource quack3AS;
    public AudioSource quack4AS;
    public AudioSource quack5AS;

    public AudioSource tapAS;

    public AudioSource grassRustle1AS;
    public AudioSource grassRustle2AS;
    public AudioSource hissAS;

    public AudioSource munchFoodAS;

    public AudioMixerGroup mixerGroup;
    public enum Quack
    {
        Quack1,
        Quack2,
        Quack3,
        Quack4,
        Quack5

    }

    private void Start()
    {
        StartCoroutine(FadeInSound(0.6f)); 
    }

    IEnumerator FadeInSound(float duration)
    {
        float elapsedTime = 0;
        float startValue = -80;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            if (mixerGroup != null)
                mixerGroup.audioMixer.SetFloat("volume", Mathf.Lerp(startValue, 0, t / duration));
            yield return null;
        }

        if (mixerGroup != null)
            mixerGroup.audioMixer.SetFloat("volume", 0);
    }


    public void PlayPressUI()
    {
        uiButtonPressedAS.Play();
    }

    public void PlayHoverUI()
    {
        uiButtonHoverAS.Play();
    }

    public void PlayRandomQuack()
    {
        PlayQuack( (Quack) UnityEngine.Random.Range(0, 4));
    }

    public void PlayQuack(Quack quackType)
    {
        switch (quackType)
        {
            case Quack.Quack1:
                PlayRandomPitchQuack(quack1AS);
                break;
            case Quack.Quack2:
                PlayRandomPitchQuack(quack2AS);
                break;
            case Quack.Quack3:
                PlayRandomPitchQuack(quack3AS);
                break;
            case Quack.Quack4:
                PlayRandomPitchQuack(quack4AS);
                break;
            case Quack.Quack5:
                PlayRandomPitchQuack(quack5AS);
                break;
            default:
                break;
        }
    }

    private void PlayRandomPitchQuack(AudioSource quackAS)
    {
        quackAS.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        quackAS.Play();
    }

    public void PlayRandomTap()
    {
        tapAS.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        tapAS.Play();
    }

    public void PlayGrass1()
    {
        grassRustle1AS.pitch = UnityEngine.Random.Range(0.8f, 1.3f);
        grassRustle1AS.Play();
    }

    public void PlayGrass2()
    {
        grassRustle2AS.pitch = UnityEngine.Random.Range(0.8f, 1.3f);
        grassRustle2AS.Play();
    }

    public void PlayHiss()
    {
        hissAS.Play();
    }

    public void PlayMunchFoodSound()
    {
        munchFoodAS.Play();
    }
}
