using DuckClicker;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatDisplay : MonoBehaviour
{
    private static PlayerStatDisplay _instance;

    public static PlayerStatDisplay Instance
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

    public TextMeshProUGUI dataText;

    public void Update()
    {

        dataText.text = @$"{NumberUtility.FormatNumber(CurrencyController.QuacksPerSecond)}";
        /*
        dataText.text = @$"Quacks per second: {ColorDouble(CurrencyController.QuacksPerSecond)}
Simple Multiplier: {ColorDouble(CurrencyController.SimpleDuckAmount)}
Magic Quack Multiplier: {ColorDouble(CurrencyController.MagicDuckBenefit)}
Bread Quack Power: {ColorDouble(CurrencyController.BreadDuckBenefit)}

Smart Food Throw Multiplier: {ColorLong(DuckAmounts.GetTotalDucks(DuckType.Clever))}
Magic Food Throw Multiplier: {ColorLong(DuckAmounts.GetTotalDucks(DuckType.Magical))}
";
        */
    }
}
