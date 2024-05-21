using UnityEngine;

namespace DuckClicker
{
    public static class CurrencyController
    {
        public static float CurrencyAmount { get; private set; }
        public static int QuackMultiplier { get; private set; }

        public static void Reset()
        {
            CurrencyAmount = 10;    
            QuackMultiplier = 1;
        }

        public static void Update()
        {
            for (int i = 0; i < 3; i++)
            {
                AddCurrency(DuckAmounts.duckCounts[DuckType.Simple][i] * (1 + DuckAmounts.duckCounts[DuckType.Bread][i]) * Time.deltaTime);
            }
        }
        
        public static void AddCurrency(float amount)
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