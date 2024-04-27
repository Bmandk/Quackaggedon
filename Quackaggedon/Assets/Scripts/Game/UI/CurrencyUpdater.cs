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
        currencyText.text = $"Quacks: {CurrencyController.CurrencyAmount:0.0}";
    }
}
