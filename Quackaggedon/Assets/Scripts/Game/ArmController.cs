using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public static ArmController Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public void OnThrow()
    {
        DuckFeeder.SelectedFeeder.ThrowBread();
        AudioController.Instance.PlayThrowSound();
    }
}
