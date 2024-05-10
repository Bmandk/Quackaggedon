using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatController : MonoBehaviour
{
    public float currencyToHaveViaCheat;
    public bool updateCurrencyToCheatValue;

    // Update is called once per frame
    void Update()
    {
        if (updateCurrencyToCheatValue)
        {
            updateCurrencyToCheatValue = false;
            CurrencyController.SetCurrency(currencyToHaveViaCheat); 
        }    
    }
}
