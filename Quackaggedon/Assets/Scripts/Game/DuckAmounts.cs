using System.Collections.Generic;

public static class DuckAmounts
{
    public static Dictionary<DuckType, int[]> duckCounts;

    public static void Reset()
    {
        int areas = 3;
        duckCounts = new Dictionary<DuckType, int[]>
        {
            { DuckType.Simple, new int[areas] },
            { DuckType.Clever, new int[areas] },
            { DuckType.Bread, new int[areas] },
            { DuckType.LunchLady, new int[areas] },
            { DuckType.Chef, new int[areas] },
            { DuckType.Magical, new int[areas] },
            { DuckType.Muscle, new int[areas] }
        };
    }
    
    public static int GetTotalDucks()
    {
        int total = 0;
        foreach (var duckType in duckCounts)
        {
            foreach (var area in duckType.Value)
            {
                total += area;
            }
        }

        return total;
    }
    
    public static int GetTotalDucks(DuckType duckType)
    {
        int total = 0;
        foreach (var area in duckCounts[duckType])
        {
            total += area;
        }

        return total;
    }
}