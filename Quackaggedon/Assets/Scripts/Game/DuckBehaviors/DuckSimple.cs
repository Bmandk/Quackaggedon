using DuckClicker;
using UnityEngine;

public class DuckSimple : MonoBehaviour, IDuck
{
    [SerializeField]
    private float _currencyBase = 1.0f;
    [SerializeField]
    private float _timeMultiplier = 1.0f;
    [SerializeField]
    private bool _givesPassiveIncome = false;
        
    private float _calculatedCurrencyPerSecond = 0.0f;
    
    
    public void OnClick()
    {
        CurrencyController.AddCurrency(_currencyBase * CurrencyController.QuackMultiplier);
    }
    
    void Start()
    {
        if (_givesPassiveIncome)
        {
            UpdateCurrency();
        }
    }
        
    public float CalculateCurrency()
    {
        return _currencyBase * _timeMultiplier;
    }

    private void UpdateCurrency()
    {
        float currency = CalculateCurrency();
            
        if (currency != _calculatedCurrencyPerSecond)
        {
            CurrencyController.AddCurrencyPerSecond(currency - _calculatedCurrencyPerSecond);
            _calculatedCurrencyPerSecond = currency;
        }
    }
}