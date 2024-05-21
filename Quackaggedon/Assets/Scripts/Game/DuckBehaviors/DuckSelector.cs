using System.Collections;
using DuckClicker;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DuckSelector : MonoBehaviour
{
    [SerializeField]
    private Material _defaultMat;
    
    [SerializeField]
    private Material _highlightMat;
    
    [SerializeField]
    private Material _selectMat;
    
    [SerializeField]
    private SpriteRenderer[] _duckSprites;
    private Animator duckAnim;
    private Coroutine _quackCoroutine;

    private bool isSelected;
    public void Select()
    {
        isSelected = true;
        ChangeDuckMaterial(_selectMat);
        SellButton.Instance.SetDuck(gameObject);

        float addAmount = DuckAmounts.GetTotalDucks() * Mathf.Pow(2, DuckAmounts.GetTotalDucks(DuckType.Bread));
        CurrencyController.AddCurrency(addAmount);
        DuckClickFeedbackHandler.Instance.DisplayDuckClick(addAmount);
        if (_quackCoroutine == null)
        {
            _quackCoroutine = StartCoroutine(QuackThenDelay());
        }
    }
    
    public void Deselect()
    {
        isSelected = false;
        ChangeDuckMaterial(_defaultMat);
        SellButton.Instance.SetDuck(null);
    }
    
    public void Hover()
    {
        ChangeDuckMaterial(_highlightMat);
    }
    
    public void Unhover()
    {
        if (!isSelected)
            ChangeDuckMaterial(_defaultMat);
    }

    private void ChangeDuckMaterial(Material newMaterial)
    {
        foreach (var sprite in _duckSprites)
        {
            sprite.material = newMaterial;
        }   
    }
    
    IEnumerator QuackThenDelay()
    {
        duckAnim.SetTrigger("Quack");
        yield return new WaitForSeconds(1);
        _quackCoroutine = null;
    }
}