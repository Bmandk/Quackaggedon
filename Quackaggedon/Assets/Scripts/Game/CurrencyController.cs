using UnityEngine;

namespace DuckClicker
{
    public static class CurrencyController
    {
        public static float CurrencyAmount { get; private set; }
        public static float CurrencyPerSecond { get; private set; }

        public static void Reset()
        {
            CurrencyAmount = 10;
            CurrencyPerSecond = 0;
        }

        public static void Update()
        {
            AddCurrency(CurrencyPerSecond * Time.deltaTime);
        }
        
        public static void AddCurrency(float amount)
        {
            CurrencyAmount += amount;
        }
        
        public static void AddCurrencyPerSecond(float amount)
        {
            CurrencyPerSecond += amount;
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