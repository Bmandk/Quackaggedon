using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsEntry : MonoBehaviour
{
    public FoodType entryFoodType;
    public DuckType duckType;

    public Image foodIcon;
    public TooltipDisplayFoodtype tooltipDisplayFoodtype;
    public Image cookbookIcon;
    public TooltipDisplayDuck TooltipDisplayDuck;

    public TextMeshProUGUI handThrown;
    public TextMeshProUGUI costFoodHandThrown;
    public TextMeshProUGUI duckThrown;
    public TextMeshProUGUI totalFoodThrown;
    public TextMeshProUGUI totalTimesDuckClicked;
    public TextMeshProUGUI clickEarnings;

    private bool _discovered = false;

    public Color hiddenIconColor;
    public Color revealedBoxColor;
    public Color hiddenCoinColor;

    public Image[] boxImages;

    public Image coinIcon;

    //public TooltipEarnedByClickedDuck tooltipEarnedByClickedDuck;

    private void Update()
    {
        if (_discovered && duckType != DuckType.Muscle)
        {
            handThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetAmountOfFoodThrownByHand(entryFoodType));
            costFoodHandThrown.text = $"<sprite name=QuacksEmoji_1> {NumberUtility.FormatNumber(PlayerFoodStats.GetTotaCostOfFoodThrownByHand(entryFoodType))}";
            duckThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetAmountOfFoodThrownBDuck(entryFoodType));
            totalFoodThrown.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTotalFoodThrown(entryFoodType));
            totalTimesDuckClicked.text = NumberUtility.FormatNumber(PlayerFoodStats.GetTimesDuckClicked(duckType));
            clickEarnings.text = $"<sprite name=QuacksEmoji_1> {NumberUtility.FormatNumber(PlayerFoodStats.GetTimesDuckClicked(duckType) * References.Instance.GetDuckData(duckType).quacksPerClick)}";
        }
    }

    public void SetStatusType(FoodType foodType, bool discovered)
    {
        entryFoodType = foodType;
        _discovered = discovered;

        FoodData entryFoodData = References.Instance.GetFoodData(entryFoodType);
        DuckData entryDuckDataeUnlocked = References.Instance.GetDuckData(DuckUnlockData.GetWhichDuckFoodUnlocks(entryFoodType));

        this.duckType = entryDuckDataeUnlocked.duckType;

        TooltipDisplayDuck.SetToDuck(entryDuckDataeUnlocked.duckType);
        tooltipDisplayFoodtype.SetToFood(entryFoodType);

        //tooltipEarnedByClickedDuck.duckType = duckType;

        if (_discovered)
        {
            foodIcon.color = Color.white;
            cookbookIcon.color = Color.white;
            foreach (var box in boxImages)
            {
                box.color = revealedBoxColor;
            }

            foodIcon.sprite = entryFoodData.foodIconRevealed;
            cookbookIcon.sprite = entryDuckDataeUnlocked.duckDisplayMiniIcon;

            coinIcon.color = Color.white;
        }
        else
        {
            handThrown.text = "-----";
            costFoodHandThrown.text = "-----";
            duckThrown.text = "-----";
            totalFoodThrown.text = "-----";
            totalTimesDuckClicked.text = "-----";
            clickEarnings.text = "-----";

            foodIcon.sprite = entryFoodData.foodIconHidden;
            cookbookIcon.sprite = entryDuckDataeUnlocked.duckDisplayMiniHidden;
            foodIcon.color = hiddenIconColor;
            cookbookIcon.color = hiddenIconColor;

            foreach (var box in boxImages)
            {
                box.color = hiddenIconColor;
            }

            coinIcon.color = hiddenCoinColor;
        }
    }
}
