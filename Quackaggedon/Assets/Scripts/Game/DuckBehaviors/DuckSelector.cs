using System;
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
    [SerializeField]
    private Animator duckAnim;
    private Coroutine _quackCoroutine;
    [SerializeField]
    private double _quacksPerClick;
    
    private DuckMovementHandler _duckMovementHandler;

    private void Awake()
    {
        _duckMovementHandler = GetComponent<DuckMovementHandler>();
    }

    //private bool isSelected;
    public void Select()
    {
        //isSelected = true;
        ChangeDuckMaterial(_selectMat);
    }


    public void Feed()
    {
        CurrencyController.AddCurrency(_quacksPerClick);
        DuckMovementHandler.lastClickedDuck = transform;
        DuckMovementHandler.lastClickTime = Time.timeSinceLevelLoad;
        DuckClickFeedbackHandler.Instance.DisplayDuckClick(_quacksPerClick);
        if (_quackCoroutine == null)
        {
            _quackCoroutine = StartCoroutine(QuackThenDelay());
        }
    }
    
    public void Deselect()
    {
        //isSelected = false;
        ChangeDuckMaterial(_defaultMat);
        //SellButton.Instance.SetDuck(null);
    }
    
    public void Hover()
    {
        if (!MouseController._selectedDucks.Contains(this))
            ChangeDuckMaterial(_highlightMat);
    }
    
    public void Unhover()
    {
        if (!MouseController._selectedDucks.Contains(this))
            ChangeDuckMaterial(_defaultMat);
    }

    private void ChangeDuckMaterial(Material newMaterial)
    {
        foreach (var sprite in _duckSprites)
        {
            if (sprite != null)
            {
                sprite.material = newMaterial;
            }
        }   
    }
    
    IEnumerator QuackThenDelay()
    {
        duckAnim.SetBool("Happy", true);
        duckAnim.SetTrigger("Quack");
        ArmController.Instance.armAnimator.SetBool("Pet",true);
        yield return new WaitForSeconds(0.05f);
        ArmController.Instance.armAnimator.SetBool("Pet", false);
        duckAnim.SetBool("Happy", false);
        _quackCoroutine = null;
    }
}