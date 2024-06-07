using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickDuckUiPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI quacksReceived;
    [SerializeField]
    private TextMeshProUGUI quacksSpentText;
    [SerializeField]
    private GameObject toDestroyWhenFxsDone;
    [SerializeField]
    private Image foodIcon;

    public void SetQuacksReceievedOnClick(double amount)
    {
        quacksReceived.text = NumberUtility.FormatNumber(amount);
    }

    public void SetFoodThrownOnClick(double amountThrown, FoodType foodThrown, double quacksSpent)
    {
        foodIcon.sprite = References.Instance.GetFoodData(foodThrown).foodIconRevealed;
        quacksReceived.text = NumberUtility.FormatNumber(amountThrown);
        quacksSpentText.text = NumberUtility.FormatNumber(quacksSpent);
    }

    public void DestroyUponCompletion()
    {
        Destroy(toDestroyWhenFxsDone.gameObject);
    }
}
