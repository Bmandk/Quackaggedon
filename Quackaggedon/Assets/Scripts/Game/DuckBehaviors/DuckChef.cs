using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;

public class DuckChef : MonoBehaviour, IDuck
{
    public void OnClick()
    {
        
    }

    private void Start()
    {
        CurrencyController.AddMultiplier();
    }
}
