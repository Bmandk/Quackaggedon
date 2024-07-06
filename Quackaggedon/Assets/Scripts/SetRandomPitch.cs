using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomPitch : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
    }
}
