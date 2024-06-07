using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class DiscoveredObjects
{
    public static List<DuckType> DuckTypesSeen { get; private set; }
    public static List<FoodType> FoodTypesSeen { get; private set; }

    public static void Reset()
    {
        DuckTypesSeen = new List<DuckType>();
        FoodTypesSeen = new List<FoodType>() { FoodType.SimpleSeeds };
    }

    public static void AddSeenDuck(DuckType type)
    {
        if (DuckTypesSeen.Contains(type))
            return;
        else
            DuckTypesSeen.Add(type);
    }

    public static void AddSeenFood(FoodType type)
    {
        if (FoodTypesSeen.Contains(type))
            return;
        else
            FoodTypesSeen.Add(type);
    }

    public static bool HasSeenDuck(DuckType type)
    {
        return DuckTypesSeen.Contains(type);
    }

    public static bool HasSeenFood(FoodType type)
    {
        return FoodTypesSeen.Contains(type);
    }
}
