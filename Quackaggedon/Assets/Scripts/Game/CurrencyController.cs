using UnityEngine;

namespace DuckClicker
{
    public static class CurrencyController
    {
        public static double CurrencyAmount { get; private set; }
        public static int QuackMultiplier { get; private set; }

        private static DuckStats duckStats;

        public static void Reset()
        {
            CurrencyAmount = 20;    
            QuackMultiplier = 1;
            duckStats = References.Instance.duckStats;
        }

        public static void Update()
        {
            AddCurrency(System.Math.Pow(DuckAmounts.duckCounts[DuckType.Simple][1] * duckStats.simpleDuckStats.quacksPerSecond, 1 + (DuckAmounts.duckCounts[DuckType.Bread][1] * duckStats.breadDuckStats.growthMultiplier)) * Time.deltaTime);
        }
        
        public static void AddCurrency(double amount)
        {
            CurrencyAmount += amount;
        }
        
        public static void RemoveCurrency(float amount)
        {
            CurrencyAmount -= amount;
        }
        
        public static bool CanAfford(float amount)
        {
            return CurrencyAmount >= amount;
        }

        public static void SetCurrency(float amount)
        {
            CurrencyAmount = amount;
        }
    }
}