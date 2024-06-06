using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static UnityEngine.ParticleSystem;

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
    public Animator armAnimator;

    public Transform handPosition;

    private int _amountToThrow;
    private FoodType _foodTypeToThrow;

    public void PerformFeedingHandAnimation(int amountToThrow, FoodType foodTypeToThrow)
    {
        _amountToThrow = amountToThrow;
        _foodTypeToThrow = foodTypeToThrow;
        armAnimator.SetTrigger("Throwing");
    }

    public void ThrownParticles()
    {
        var foodPrefab = References.Instance.GetFoodData(_foodTypeToThrow).foodPrefab;
        var inst = Instantiate(foodPrefab, ArmController.Instance.handPosition.position, foodPrefab.transform.rotation, References.Instance.particleParent);
        inst.GetComponent<ParticleSystem>().Emit(_amountToThrow);
    }

    /*
    public void OnThrow()
    {
        DuckFeeder.SelectedFeeder.ThrowBread(true);
        AudioController.Instance.PlayThrowSound();
    }
    */
}
