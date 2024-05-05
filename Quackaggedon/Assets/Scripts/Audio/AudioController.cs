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

    [SerializeField]
    private AudioSource[] _quackSoundAS;

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

}
