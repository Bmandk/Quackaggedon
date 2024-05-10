using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    private static ArmController _instance;

    public static ArmController Instance
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

    [SerializeField]
    private Animator armAnimator;

    public void PerformFeedingHandAnimation()
    {
        armAnimator.SetTrigger("Throwing");
    }

    public void OnThrow()
    {
        DuckFeeder.SelectedFeeder.ThrowBread();
        AudioController.Instance.PlayThrowSound();
    }
}
