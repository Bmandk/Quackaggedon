using UnityEngine;

namespace DuckClicker
{
    public static class CurrencyController
    {
        public static int CurrencyAmount { get; private set; }

        public static void Reset()
        {
            CurrencyAmount = 0;
        }
        
        public static void AddCurrency(int amount)
        {
            CurrencyAmount += amount;
        }
        
        public static void RemoveCurrency(int amount)
        {
            CurrencyAmount -= amount;
        }
        
        public static bool CanAfford(int amount)
        {
            return CurrencyAmount >= amount;
        }
    }
}