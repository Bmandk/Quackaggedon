using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public void PlayThrowSound()
    {
        _throwSoundAS.pitch = UnityEngine.Random.Range(0.8f, 1.3f);
        _throwSoundAS.Play();
    }

}
