using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using TMPro;
using UnityEngine;

public class CurrencyUpdater : MonoBehaviour
{
    private TMP_Text currencyText;
    
    private void Start()
    {
        currencyText = GetComponent<TMP_Text>();
    }
    
    private void Update()
    {
        // Round down to 1 decimal place
        // If we simply round, the currency could round up, but the player still cannot afford food
        float currencyAmount = Mathf.Floor(CurrencyController.CurrencyAmount * 10) / 10;
        currencyText.text = $"{currencyAmount:0.0}";
    }
}
