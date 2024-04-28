using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUISoundHandler : MonoBehaviour
{
    private static MainMenuUISoundHandler _instance;

    public static MainMenuUISoundHandler Instance { get { return _instance; } }


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
    public enum Quack
    {
        Quack1,
        Quack2,
        Quack3,
        Quack4,
        Quack5

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
}
