using UnityEngine;

namespace DuckClicker
{
    public static class CurrencyController
    {
        public static double CurrencyAmount { get; private set; }
        public static int QuackMultiplier { get; private set; }

        private static DuckStats duckStats;

        public static long SimpleDuckAmount { get; private set; }
        public static long BreadDuckAmount { get; private set; }
        public static long MagicalDuckAmount { get; private set; }
        public static double MagicDuckBenefit { get; private set; }
        public static double BreadDuckBenefit { get; private set; }
        public static double QuacksPerSecond { get; private set; }

        public static void Reset()
        {
            CurrencyAmount = 20;
            QuackMultiplier = 1;
            duckStats = References.Instance.duckStats;
        }

        public static void Update()
        {
            // =POW(amount * qps, (POW(bonus*(1+magic*magicmult)+1,1/(bonuslimit*(1+magic*magiclimit)))))
            SimpleDuckAmount = DuckAmounts.duckCounts[DuckType.Simple][1];
            BreadDuckAmount = DuckAmounts.duckCounts[DuckType.Bread][1];
            MagicalDuckAmount = DuckAmounts.duckCounts[DuckType.Magical][1];

            MagicDuckBenefit = 1 + MagicalDuckAmount * duckStats.magicalDuckStats.multiplier;
            BreadDuckBenefit = System.Math.Pow(1 + BreadDuckAmount * duckStats.breadDuckStats.growthMultiplier * MagicDuckBenefit, 1 / (duckStats.breadDuckStats.limitMultiplier * (1 + MagicalDuckAmount * duckStats.magicalDuckStats.limitMultiplier)));
            QuacksPerSecond = System.Math.Pow(SimpleDuckAmount * duckStats.simpleDuckStats.quacksPerSecond, BreadDuckBenefit);
            AddCurrency(QuacksPerSecond * Time.deltaTime);
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

        public static double GetQuackBonus(DuckType duckType)
        {
            switch (duckType)
            {
                case DuckType.Simple:
                    return SimpleDuckAmount;
                case DuckType.Bread:
                    return BreadDuckBenefit;
                case DuckType.Magical:
                    return MagicDuckBenefit;
                default:
                    return 0;
            }
        }

        public static long GetFoodBonus(DuckType duckType)
        {
            switch(duckType)
            {
                case DuckType.Clever:
                    return DuckAmounts.GetTotalDucks(DuckType.Clever);
                case DuckType.Magical:
                    return DuckAmounts.GetTotalDucks(DuckType.Magical);
                default:
                    return 0;
            }
        }
    }
}