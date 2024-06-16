using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuckEntryInstanceHandler : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image duckIcon;
    public TextMeshProUGUI skillText;
    public TextMeshProUGUI duckAmount;
    public Button detailsButton;

    [SerializeField]
    private Sprite hiddenDuck;
    private string hiddenName = "Undiscovered duck";
    private string hiddenSkill = "Unknown skill";

    public Color hiddenDuckColor;
    public Color revealedDuckColor = Color.white;

    public DuckType duckTypeOfCard;

    public Button button;

    public void SetEntryToDuck(DuckType duckType)
    {
        duckTypeOfCard = duckType;

        var duckData = References.Instance.GetDuckData(duckType);

        duckIcon.sprite = duckData.duckDisplayIconRevealed;
        duckIcon.color = revealedDuckColor;
        nameText.text = duckData.duckDisplayName;
        skillText.text = duckData.duckEffectDescription;

        duckAmount.text = ColorLong(DuckAmounts.duckCounts[duckType][1]).ToString();
        /*breadBonus.text = ColorLong(CurrencyController.GetFoodBonus(duckType)).ToString();
        quackBonus.text = ColorDouble(CurrencyController.GetQuackBonus(duckType)).ToString();*/   
        
        detailsButton.interactable = true;
    }

    public void SetEntryToUndiscoveredDuck(DuckType duckType)
    {
        var duckData = References.Instance.GetDuckData(duckType);

        duckIcon.sprite = duckData.duckDisplayIconHidden;
        duckIcon.color = hiddenDuckColor;

        nameText.text = hiddenName;
        skillText.text = hiddenSkill;        

        button.interactable = false;
    }

    public void SetDuckEnumTypeTo(DuckType duckType)
    {
        duckTypeOfCard = duckType;
    }

    private string ColorDouble(double number)
    {
        if (number <= 1)
        {
            return number.ToString();
        }
        else
        {
            return $"<color=green>{number}</color>";
        }
    }

    private string ColorLong(long number)
    {
        if (number <= 1)
        {
            return number.ToString();
        }
        else
        {
            return $"<color=green>{number}</color>";
        }
    }

    public void OpenDetails()
    {
        References.Instance.duckopediaDetails.SetEntryToDuck(duckTypeOfCard);
    }
}
