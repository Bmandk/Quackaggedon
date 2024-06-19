using System;
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

    private void Update()
    {
        handThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetAmountOfFoodThrownByHand(entryFoodType));
        costFoodHandThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTotaCostOfFoodThrownByHand(entryFoodType));
        duckThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetAmountOfFoodThrownBDuck(entryFoodType));
        totalFoodThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTotalFoodThrown(entryFoodType));

        FoodData entryFoodData = References.Instance.GetFoodData(entryFoodType);
        DuckData entryDuckDataeUnlocked = References.Instance.GetDuckData(DuckUnlockData.GetWhichDuckFoodUnlocks(entryFoodType));

        totalTimesDuckClicked.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTimesDuckClicked(entryDuckDataeUnlocked.duckType));

        foodIcon.sprite = entryFoodData.foodIconRevealed;
        cookbookIcon.sprite = entryDuckDataeUnlocked.duckDisplayMiniIcon;

        TooltipDisplayDuck.SetToDuck(entryDuckDataeUnlocked.duckType);
        tooltipDisplayFoodtype.SetToFood(entryFoodType);
    }

    public void UpdateCookbookEntryValues(FoodType foodType)
    {
        entryFoodType = foodType;
    }
}
