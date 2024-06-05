using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStarter : MonoBehaviour
{
    private static EndStarter _instance;
    public CameraShake camShake;

    public static EndStarter Instance
    {
        get { return _instance; }
    }


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

    public GameObject megaDuckAppears;
    public void StartEnd()
    {
        megaDuckAppears.SetActive(true);
        StartCameraShake();
    }

    private void StartCameraShake()
    {
        StartCoroutine(camShake.Shake(4, 0.5f));
     //   throw new NotImplementedException();
    }
}
