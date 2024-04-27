using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using TMPro;
using UnityEngine;

public class FoodUpdater : MonoBehaviour
{
    private TMP_Text foodText;
    private DuckFeeder duckFeeder;
    
    private void Start()
    {
        foodText = GetComponent<TMP_Text>();
        duckFeeder = FindObjectOfType<DuckFeeder>();
    }
    
    private void Update()
    {
        foodText.text = $"Food: {duckFeeder.foodAmount}";
    }
}
