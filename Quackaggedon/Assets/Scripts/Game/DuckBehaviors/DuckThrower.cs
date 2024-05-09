using System;
using DuckClicker;
using UnityEngine;

public class DuckThrower : MonoBehaviour, IDuck
{
    public float multiplier = 1.0f;
    private static float sMultiplier; // 🤢🤢🤢🤢🤢🤢🤢🤢
    private static int amountOfDucks = 0;
    public static float speed;

    private void Awake()
    {
        sMultiplier = multiplier; // 🤢🤢🤢🤢🤢🤢🤢🤢
    }

    public void OnClick()
    {
        
    }

    private void OnEnable()
    {
        SetDuckAmount(amountOfDucks + 1);
    }
    
    private void OnDisable()
    {
        SetDuckAmount(amountOfDucks - 1);
    }

    public static void SetDuckAmount(int amount)
    {
        amountOfDucks = amount;
        speed = Mathf.Sqrt(amountOfDucks) * sMultiplier;
    }
}