using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlaySound : MonoBehaviour
{
    public AudioSource biteAS;

    public void PlayBiteSound()
    {
        biteAS.Play();
    }
}
