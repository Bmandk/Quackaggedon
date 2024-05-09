using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using TMPro;
using UnityEngine;

public class BuyFoodButton : MonoBehaviour
{
    public DuckFeeder duckFeeder;
    
    private TMP_Text buttonText;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        buttonText.text = $"$ {duckFeeder.foodCost}";
    }

    private void Update()
    {
        if (duckFeeder != null)
        {
            GetComponent<UnityEngine.UI.Button>().interactable = CurrencyController.CanAfford(duckFeeder.foodCost);
        }
    }

    public void OnClick()
    {
        if (duckFeeder != null)
        {
            duckFeeder.BuyFood();
        }
    }
}
