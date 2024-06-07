using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;
using UnityEngine.UIElements;

public class DuckData : MonoBehaviour
{
    public DuckType duckType;
    public string duckDisplayName;
    [TextArea(5, 20)]
    public string duckEffectDescription;
    public Sprite duckDisplayIcon;
    public GameObject duckPrefab;

    public static List<Transform> chefDucks;

    private void OnDestroy()
    {
        DuckAmounts.duckCounts[duckType][AreaSettings.CurrentArea.AreaIndex]--;
        
        foreach (DuckFeeder duckFeeder in FindObjectsOfType<DuckFeeder>())
        {
            duckFeeder.Refresh();
        }
    }

    private void OnEnable()
    {
        if (duckType == DuckType.Chef)
        {
            chefDucks.Add(transform);
        }
    }
    
    private void OnDisable()
    {
        if (duckType == DuckType.Chef)
        {
            chefDucks.Remove(transform);
        }
    }
}
