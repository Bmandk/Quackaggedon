using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DiscoveredObjects
{
    public static List<DuckType> DuckTypesSeen { get; private set; }
    public static List<FoodType> FoodTypesSeen { get; private set; }

    public static void Reset()
    {
        DuckTypesSeen = new List<DuckType>();
        FoodTypesSeen = new List<FoodType>();
    }

    public static void AddSeenDuck(DuckType type)
    {
        DuckTypesSeen.Add(type);
    }

    public static void AddSeenFood(FoodType type)
    {
        FoodTypesSeen.Add(type);
    }
}
