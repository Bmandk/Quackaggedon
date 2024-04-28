using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayBreath : MonoBehaviour
{
    public AudioSource breathAS;

    public void PlayBreathSound()
    {
        breathAS.Play();
    }
}
