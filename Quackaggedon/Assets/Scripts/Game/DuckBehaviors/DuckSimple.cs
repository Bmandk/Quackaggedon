using DuckClicker;
using System.Collections;
using UnityEngine;

public class DuckSimple : MonoBehaviour, IDuck
{
    [SerializeField]
    private float _currencyBase = 1.0f;
    [SerializeField]
    private float _timeMultiplier = 1.0f;
    [SerializeField]
    private bool _givesPassiveIncome = false;
    [SerializeField]
    private Animator duckAnim;

    private float _calculatedCurrencyPerSecond = 0.0f;
    private Coroutine _quackCoroutine;
    
    public void OnClick()
    {
        float addAmount = _currencyBase * CurrencyController.QuackMultiplier;
        CurrencyController.AddCurrency(addAmount);
        DuckClickFeedbackHandler.Instance.DisplayDuckClick(addAmount);
        //if (_quackCoroutine == null)
        {
            _quackCoroutine = StartCoroutine(QuackThenDelay());
        }
    }

    IEnumerator QuackThenDelay()
    {
        duckAnim.SetTrigger("Quack");
        yield return new WaitForSeconds(1);
        //_quackCoroutine = null;
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