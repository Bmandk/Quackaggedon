using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckFoodAmount : MonoBehaviour, IDuck
{
    public static int smartDuckCount = 0;
    
    public void OnClick()
    {
        
    }

    private void Start()
    {
        smartDuckCount++;
    }
}
