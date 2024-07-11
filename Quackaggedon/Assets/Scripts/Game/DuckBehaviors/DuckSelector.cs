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

    public Animator duckAnim;
    private Coroutine _quackCoroutine;

    private double _quacksPerClick;
    
    private DuckMovementHandler _duckMovementHandler;
    private DuckData _duckData;
    
    private bool _isTutorialActive;

    private void Awake()
    {
        _quacksPerClick = transform.GetComponent<DuckData>().quacksPerClick;
        _duckMovementHandler = GetComponent<DuckMovementHandler>();
        _duckData = GetComponent<DuckData>();
    }
    
    private void Start()
    {
        if (_duckData.duckType == DuckType.Simple && DuckAmounts.GetTotalDucks(DuckType.Simple) == 1)
        {
            TutorialController.ShowTutorialArrowUI(transform, new Vector2(0, 100), TutorialArrowDirection.Down, 1, true);
            _isTutorialActive = true;
        }
    }

    //private bool isSelected;
    public void Select()
    {
        //isSelected = true;
        ChangeDuckMaterial(_selectMat);
    }


    public void Feed()
    {
        if (_isTutorialActive)
        {
            TutorialController.HideTutorialArrow();
            _isTutorialActive = false;
        }
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
        ArmController.Instance.armAnimator.SetTrigger("Pet2");
        //ArmController.Instance.armAnimator.SetBool("Pet",true);
        yield return new WaitForSeconds(0.05f);
        //ArmController.Instance.armAnimator.SetBool("Pet", false);
        duckAnim.SetBool("Happy", false);
        _quackCoroutine = null;
    }
}