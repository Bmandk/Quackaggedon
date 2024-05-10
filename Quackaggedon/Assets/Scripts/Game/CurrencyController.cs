using UnityEngine;

namespace DuckClicker
{
    public static class CurrencyController
    {
        public static float CurrencyAmount { get; private set; }
        public static float CurrencyPerSecond { get; private set; }
        public static int QuackMultiplier { get; private set; }

        public static void Reset()
        {
            CurrencyAmount = 10;
            CurrencyPerSecond = 0;
            QuackMultiplier = 1;
        }

        public static void Update()
        {
            AddCurrency(CurrencyPerSecond * Time.deltaTime * QuackMultiplier * (DuckBonus.AmountOfDucks + 1));
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

        public static void AddMultiplier()
        {
            QuackMultiplier++;
        }
    }
}