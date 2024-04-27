using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;

public class BuyFoodButton : MonoBehaviour
{
    public void BuyFood()
    {
        DuckFeeder duckFeeder = FindObjectOfType<DuckFeeder>();
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
