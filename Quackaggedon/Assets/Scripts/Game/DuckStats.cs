using System;
using System.Collections;
using System.Collections.Generic;
using DuckClicker;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "DuckStats", menuName = "DuckStats")]
[Serializable]
public class DuckStats : ScriptableObject
{
    public SimpleDuckStats simpleDuckStats;
    public CleverDuckStats cleverDuckStats;
    public BreadDuckStats breadDuckStats;
    public LunchLadyDuckStats lunchLadyDuckStats;
    public ChefDuckStats chefDuckStats;
    public MagicalDuckStats magicalDuckStats;
    public MuscleDuckStats muscleDuckStats;

    public DuckFeederStats GetDuckFeederStats(DuckType duckType)
    {
        switch (duckType)
        {
            case DuckType.Simple:
                return simpleDuckStats.duckFeederStats;
            case DuckType.Bread:
                return breadDuckStats.duckFeederStats;
            case DuckType.Chef:
                return chefDuckStats.duckFeederStats;
            case DuckType.Magical:
                return magicalDuckStats.duckFeederStats;
            case DuckType.Muscle:
                return muscleDuckStats.duckFeederStats;
            case DuckType.Clever:
                return cleverDuckStats.duckFeederStats;
            case DuckType.LunchLady:
                return lunchLadyDuckStats.duckFeederStats;
            case DuckType.Mafia:
            default:
                throw new ArgumentOutOfRangeException(nameof(duckType), duckType, null);
        }
    }
}

[Serializable]
public struct DuckFeederStats
{
    public double baseFoodPerDuck;
    public double growthRate;
    public double foodCost;
            
    public double CalculateCost(long ducksSpawned)
    {
        return System.Math.Round(baseFoodPerDuck * Math.Pow(growthRate, ducksSpawned));
    }
}

[Serializable]
public struct SimpleDuckStats
{
    public DuckFeederStats duckFeederStats;
    public double quacksPerSecond;
    public double quacksPerClick;
}

[Serializable]
public struct BreadDuckStats
{
    public DuckFeederStats duckFeederStats;
    public double growthMultiplier;
    public double limitMultiplier;
}

[Serializable]
public struct ChefDuckStats
{
    public DuckFeederStats duckFeederStats;
    public double timeGrowthRate;
    public double amountOffset;
    public double minTime;
}

[Serializable]
public struct MagicalDuckStats
{
    public DuckFeederStats duckFeederStats;
    public double quackMultiplier;
    public double quackLimitMultiplier;
    public double cleverMultiplier;
    public double chefMultiplier;
}

[Serializable]
public struct MuscleDuckStats
{
    public DuckFeederStats duckFeederStats;
}

[Serializable]
public struct CleverDuckStats
{
    public DuckFeederStats duckFeederStats;
    public double minFoodPerDuck;
    public double foodAmountGrowthRate;
    public double foodAmountMultiplier;
}

[Serializable]
public struct LunchLadyDuckStats
{
    public DuckFeederStats duckFeederStats;
}