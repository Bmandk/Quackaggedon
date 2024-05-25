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

    private void OnDestroy()
    {
        DuckAmounts.duckCounts[duckType][AreaSettings.CurrentArea.AreaIndex]--;
        
        foreach (DuckFeeder duckFeeder in FindObjectsOfType<DuckFeeder>())
        {
            duckFeeder.Refresh();
        }
    }
}
