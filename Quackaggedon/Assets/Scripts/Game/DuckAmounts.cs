using System.Collections.Generic;

public static class DuckAmounts
{
    public static Dictionary<DuckType, long[]> duckCounts;

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
}