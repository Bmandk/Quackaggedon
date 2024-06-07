using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookbookEntry : MonoBehaviour
{
    public FoodType entryFoodType;

    public Image foodIcon;
    public Image cookbookIcon;

    public TextMeshProUGUI handThrown;
    public TextMeshProUGUI costFoodHandThrown;
    public TextMeshProUGUI duckThrown;
    public TextMeshProUGUI totalFoodThrown;

    public void UpdateCookbookEntryValues(FoodType foodType)
    {
        entryFoodType = foodType;

        handThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetAmountOfFoodThrownByHand(foodType));
        costFoodHandThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTotaCostOfFoodThrownByHand(foodType));
        duckThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetAmountOfFoodThrownBDuck(foodType));
        totalFoodThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTotalFoodThrown(foodType));



    }
}
