using UnityEngine;

namespace DuckClicker
{
    public static class CurrencyController
    {
        public static float CurrencyAmount { get; private set; }

        public static void Reset()
        {
            CurrencyAmount = 0;
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
    }
}