using System.Collections.Generic;

public static class DuckAmounts
{
    public static Dictionary<DuckType, long[]> duckCounts;
    public static Dictionary<DuckType, int> hutAmounts;

    public static void Reset()
    {
        int areas = 3;
        duckCounts = new Dictionary<DuckType, long[]>
        {
            { DuckType.Simple, new long[areas] },
            { DuckType.Clever, new long[areas] },
            { DuckType.Bread, new long[areas] },
            { DuckType.LunchLady, new long[areas] },
            { DuckType.Chef, new long[areas] },
            { DuckType.Magical, new long[areas] },
            { DuckType.Muscle, new long[areas] }
        };
        
        hutAmounts = new Dictionary<DuckType, int>
        {
            { DuckType.Simple, 0 },
            { DuckType.Clever, 0 },
            { DuckType.Bread, 0 },
            { DuckType.LunchLady, 0 },
            { DuckType.Chef, 0 },
            { DuckType.Magical, 0 },
            { DuckType.Muscle, 0 }
        };
    }
    
    public static long GetTotalDucks()
    {
        long total = 0;
        foreach (var duckType in duckCounts)
        {
            foreach (var area in duckType.Value)
            {
                total += area;
            }
        }

        return total;
    }
    
    public static long GetTotalDucks(DuckType duckType)
    {
        long total = 0;
        foreach (var area in duckCounts[duckType])
        {
            total += area;
        }

        return total;
    }
    
    public static long GetTotalDucks(int area)
    {
        long total = 0;
        foreach (var duckType in duckCounts)
        {
            total += duckType.Value[area];
        }

        return total;
    }
    
    public static long GetTotalDucksInPond()
    {
        long total = 0;

        foreach (var duckTypeList in DuckData.duckObjects)
        {
            total += duckTypeList.Value.Count;
        }
        
        return total;
    }
    
    public static long GetTotalDucksInPond(DuckType duckType)
    {
        return DuckData.duckObjects[duckType].Count;
    }
}