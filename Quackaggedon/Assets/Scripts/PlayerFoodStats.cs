using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class PlayerFoodStats
{
    public static Dictionary<FoodType, double> FoodThrownByHand { get; private set; }
    public static Dictionary<FoodType, double> FoodThrownByDuck { get; private set; }
    public static Dictionary<FoodType, double> CostOfFoodThrownByHand { get; private set; }
    public static Dictionary<FoodType, double> TotalFoodThrown { get; private set; }

    public static Dictionary<DuckType, double> TotalTimesDuckClicked { get; private set; }

    public static void Reset()
    {
        FoodThrownByHand = new Dictionary<FoodType, double>();
        FoodThrownByDuck = new Dictionary<FoodType, double>();
        CostOfFoodThrownByHand = new Dictionary<FoodType, double>();
        TotalFoodThrown = new Dictionary<FoodType, double>();
        TotalTimesDuckClicked = new Dictionary<DuckType, double>();
    }

    public static void AddTimesDuckClicked(DuckType duckType, double amount)
    {
        if (TotalTimesDuckClicked.ContainsKey(duckType))
        {
            TotalTimesDuckClicked[duckType] += amount;
        }
        else
        {
            TotalTimesDuckClicked.Add(duckType, amount);
        }
    }

    public static double GetTimesDuckClicked(DuckType duckType)
    {
        if (TotalTimesDuckClicked.ContainsKey(duckType))
            return (TotalTimesDuckClicked[duckType]);
        else 
            return 0;
    }

    public static void AddToTotalFoodThrown(FoodType foodType, double amountThrown)
    {
        if (TotalFoodThrown.ContainsKey(foodType))
        {
            TotalFoodThrown[foodType] += amountThrown;
        }
        else
        {
            TotalFoodThrown.Add(foodType, amountThrown);
        }
    }

    public static double GetTotalFoodThrown(FoodType foodType)
    {
        if (TotalFoodThrown.ContainsKey(foodType))
            return TotalFoodThrown[foodType];
        return 0;
    }

    public static void AddTotalCostOfFoodThrownByHand(FoodType foodType, double amountThrown)
    {
        if (CostOfFoodThrownByHand.ContainsKey(foodType))
        {
            CostOfFoodThrownByHand[foodType] += amountThrown;
        }
        else
        {
            CostOfFoodThrownByHand.Add(foodType, amountThrown);
        }
    }

    public static double GetTotaCostOfFoodThrownByHand(FoodType foodType)
    {
        if (CostOfFoodThrownByHand.ContainsKey(foodType))
            return CostOfFoodThrownByHand[foodType];
        return 0;
    }

    public static void AddHandThrownFood(FoodType foodType, double amountThrown)
    {
        if (FoodThrownByHand.ContainsKey(foodType))
        {
            FoodThrownByHand[foodType] += amountThrown;
        }
        else 
        {
            FoodThrownByHand.Add(foodType, amountThrown);
        }
    }

    public static double GetAmountOfFoodThrownByHand(FoodType foodType)
    {
        if (FoodThrownByHand.ContainsKey(foodType))
            return FoodThrownByHand[foodType];
        return 0;
    }

    public static void AddDuckThrownFood(FoodType foodType, double amountThrown)
    {
        if (FoodThrownByDuck.ContainsKey(foodType))
        {
            FoodThrownByDuck[foodType] += amountThrown;
        }
        else
        {
            FoodThrownByDuck.Add(foodType, amountThrown);
        }
    }
    public static double GetAmountOfFoodThrownBDuck(FoodType foodType)
    {
        if (FoodThrownByDuck.ContainsKey(foodType))
            return FoodThrownByDuck[foodType];
        return 0;
    }
}
