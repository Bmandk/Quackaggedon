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
            // =POW(amount * qps, (POW(bonus*(1+magic*magicmult)+1,1/(bonuslimit*(1+magic*magiclimit)))))
            long simpleDuckAmount = DuckAmounts.duckCounts[DuckType.Simple][1];
            long breadDuckAmount = DuckAmounts.duckCounts[DuckType.Bread][1];
            long magicalDuckAmount = DuckAmounts.duckCounts[DuckType.Magical][1];
            
            double magicDuck = 1 + magicalDuckAmount * duckStats.magicalDuckStats.multiplier;
            double breadDuck = System.Math.Pow(1 + breadDuckAmount * duckStats.breadDuckStats.growthMultiplier * magicDuck, 1 / (duckStats.breadDuckStats.limitMultiplier * (1 + magicalDuckAmount * duckStats.magicalDuckStats.limitMultiplier)));
            double quacksPerSecond = System.Math.Pow(simpleDuckAmount * duckStats.simpleDuckStats.quacksPerSecond, breadDuck);
            AddCurrency(quacksPerSecond * Time.deltaTime); 
        }
        
        public static void AddCurrency(double amount)
        {
            CurrencyAmount += amount;
        }
        
        public static void RemoveCurrency(double amount)
        {
            CurrencyAmount -= amount;
        }
        
        public static bool CanAfford(double amount)
        {
            return CurrencyAmount >= amount;
        }

        public static void SetCurrency(double amount)
        {
            CurrencyAmount = amount;
        }
    }
}