using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    public static SellButton Instance { get; private set; }
    
    private GameObject _currentDuck;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Multiple SellButton instances found!");
        }
        
        Instance = this;
    }
    
    public void SetDuck(GameObject duck)
    {
        _currentDuck = duck;
        transform.GetChild(0).gameObject.SetActive(_currentDuck != null);
    }
    
    public void SellDuck()
    {
        if (_currentDuck != null)
        {
            Destroy(_currentDuck);
        }
    }
}
