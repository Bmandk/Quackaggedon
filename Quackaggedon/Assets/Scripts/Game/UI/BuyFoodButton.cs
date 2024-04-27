using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;

public class BuyFoodButton : MonoBehaviour
{
    private DuckFeeder _duckFeeder;
    
    private void Start()
    {
        _duckFeeder = FindObjectOfType<DuckFeeder>();
    }
    
    private void Update()
    {
        if (_duckFeeder != null)
        {
            GetComponent<UnityEngine.UI.Button>().interactable = CurrencyController.CanAfford(_duckFeeder.foodCost);
        }
    }

    public void BuyFood()
    {
        if (_duckFeeder != null)
        {
            if (CurrencyController.CanAfford(_duckFeeder.foodCost))
            {
                CurrencyController.RemoveCurrency(_duckFeeder.foodCost);
                _duckFeeder.foodAmount++;
            }
        }
    }
}
