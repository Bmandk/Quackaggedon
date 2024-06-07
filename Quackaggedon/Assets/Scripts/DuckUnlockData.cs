using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DuckUnlockData 
{
    public static (FoodType, DuckType)[] FoodThatUnlocksDuck = new (FoodType, DuckType)[]
    {
        (FoodType.SimpleSeeds,DuckType.Simple),
        (FoodType.Brocolli,DuckType.Clever),
        (FoodType.Grains,DuckType.Bread),
        (FoodType.GourmetCaviar,DuckType.Chef),
        (FoodType.MagicPeas,DuckType.Magical),
        (FoodType.ProteinShake,DuckType.Muscle),
    };

    public static int GetUnlockOrder(DuckType duckType)
    {
        for (int i = 0; i < FoodThatUnlocksDuck.Length; i++) 
        {
            if (FoodThatUnlocksDuck[i].Item2 == duckType)
                return i;
        }
        Debug.LogWarning($"duckType {duckType} doesn't exist in the unlock-progression. Have you forgotten to add it?");
        return -1;
    }

    public static DuckType GetWhichDuckFoodUnlocks(FoodType foodType)
    {
        foreach (var unlock in FoodThatUnlocksDuck)
        {
            if (unlock.Item1 == foodType)
                return unlock.Item2;
        }
        Debug.LogWarning($"{foodType} doesn't exist in the unlock-progression. Have you forgotten to add it?");
        return DuckType.Simple;
    }

    public static FoodType GetWhichFoodsNeededToUnlockDuck(DuckType duckType)
    {
        foreach (var unlock in FoodThatUnlocksDuck)
        {
            if (unlock.Item2 == duckType)
                return unlock.Item1;
        }
        Debug.LogWarning($"{duckType} doesn't exist in the unlock-progression. Have you forgotten to add it?");
        return FoodType.SimpleSeeds;
    }

    public static int GetFoodIndexUnlockedAfterThisOne(FoodType foodType)
    {
        int index = -1;
        for (int i = 0; i < FoodThatUnlocksDuck.Length; i++)
        {
            if (FoodThatUnlocksDuck[i].Item1 == foodType)
                index = i+1;
        }

        if (index <= FoodThatUnlocksDuck.Length)
            return index;
        else    //There is no unlock afterwards
            return -1;
    }

    public static FoodType GetFoodUnlockedAfterThisOne(FoodType foodType)
    {
        int index = -1;
        for (int i = 0; i < FoodThatUnlocksDuck.Length; i++)
        {
            if (FoodThatUnlocksDuck[i].Item1 == foodType)
                index = i + 1;
        }

        if (index <= FoodThatUnlocksDuck.Length)
            return FoodThatUnlocksDuck[index].Item1;
        else    //There is no unlock afterwards
            return FoodType.SimpleSeeds;
    }

}
