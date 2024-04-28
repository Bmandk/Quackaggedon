using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;

public class BuyFoodButton : MonoBehaviour
{
    public DuckFeeder duckFeeder;
    
    private void Update()
    {
        if (duckFeeder != null)
        {
            GetComponent<UnityEngine.UI.Button>().interactable = CurrencyController.CanAfford(duckFeeder.foodCost);
        }
    }

    public void BuyFood()
    {
        if (duckFeeder != null)
        {
            if (CurrencyController.CanAfford(duckFeeder.foodCost))
            {
                CurrencyController.RemoveCurrency(duckFeeder.foodCost);
                duckFeeder.foodAmount++;
            }
        }
    }
}
