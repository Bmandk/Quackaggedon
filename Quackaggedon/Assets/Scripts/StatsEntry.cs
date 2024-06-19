using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsEntry : MonoBehaviour
{
    public FoodType entryFoodType;

    public Image foodIcon;
    public TooltipDisplayFoodtype tooltipDisplayFoodtype;
    public Image cookbookIcon;
    public TooltipDisplayDuck TooltipDisplayDuck;

    public TextMeshProUGUI handThrown;
    public TextMeshProUGUI costFoodHandThrown;
    public TextMeshProUGUI duckThrown;
    public TextMeshProUGUI totalFoodThrown;
    public TextMeshProUGUI totalTimesDuckClicked;

    public void UpdateCookbookEntryValues(FoodType foodType)
    {
        entryFoodType = foodType;

        handThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetAmountOfFoodThrownByHand(foodType));
        costFoodHandThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTotaCostOfFoodThrownByHand(foodType));
        duckThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetAmountOfFoodThrownBDuck(foodType));
        totalFoodThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTotalFoodThrown(foodType));

        FoodData entryFoodData = References.Instance.GetFoodData(foodType);
        DuckData entryDuckDataeUnlocked = References.Instance.GetDuckData(DuckUnlockData.GetWhichDuckFoodUnlocks(foodType));

        totalTimesDuckClicked.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTimesDuckClicked(entryDuckDataeUnlocked.duckType));

        foodIcon.sprite = entryFoodData.foodIconRevealed;
        cookbookIcon.sprite = entryDuckDataeUnlocked.duckDisplayMiniIcon;

        TooltipDisplayDuck.SetToDuck(entryDuckDataeUnlocked.duckType);
        tooltipDisplayFoodtype.SetToFood(foodType);

    }
}
