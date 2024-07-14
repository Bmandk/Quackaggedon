using Steamworks;
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

        public static double BeforeSimpleDuck;
        public static double AfterSimpleDuck;
        public static double BeforeBreadDuck;
        public static double AfterBreadDuck;
        public static double BeforeMagicalDuck;
        public static double AfterMagicalDuck;
        
        private static bool _didShowSimpleDuck;
        private static bool _didShowBreadDuck;
        private static bool _didShowMagicalDuck;

        private static bool _didShowAchievement1 = false;
        private static bool _didShowAchievement2 = false;
        private static bool _didShowAchievement3 = false;
        private static bool _didShowAchievement4 = false;

        public static void Reset()
        {
            CurrencyAmount = 20;
            QuackMultiplier = 1;
            duckStats = References.Instance.duckStats;
            
            SimpleDuckAmount = 0;
            BreadDuckAmount = 0;
            MagicalDuckAmount = 0;
            MagicDuckBenefit = 1;
            BreadDuckBenefit = 1;
            QuacksPerSecond = 0;
                
            BeforeSimpleDuck = 0;
            AfterSimpleDuck = 0;
            BeforeBreadDuck = 0;
            AfterBreadDuck = 0;
            BeforeMagicalDuck = 0;
            AfterMagicalDuck = 0;
            
            _didShowSimpleDuck = false;
            _didShowBreadDuck = false;
            _didShowMagicalDuck = false;
        }

        public static void Update()
        {
            // =POW(amount * qps, (POW(bonus*(1+magic*magicmult)+1,1/(bonuslimit*(1+magic*magiclimit)))))
            SimpleDuckAmount = DuckAmounts.duckCounts[DuckType.Simple][1];
            BreadDuckAmount = DuckAmounts.duckCounts[DuckType.Bread][1];
            MagicalDuckAmount = DuckAmounts.duckCounts[DuckType.Magical][1];
            
            if (!_didShowSimpleDuck && SimpleDuckAmount > 0)
            {
                BeforeSimpleDuck = QuacksPerSecond;
            }
            
            if (!_didShowBreadDuck && BreadDuckAmount > 0)
            {
                BeforeBreadDuck = QuacksPerSecond;
            }
            
            if (!_didShowMagicalDuck && MagicalDuckAmount > 0)
            {
                BeforeMagicalDuck = QuacksPerSecond;
            }

            MagicDuckBenefit = 1 + MagicalDuckAmount * duckStats.magicalDuckStats.quackMultiplier;
            BreadDuckBenefit = System.Math.Pow(1 + BreadDuckAmount * duckStats.breadDuckStats.growthMultiplier * MagicDuckBenefit, 1 / (duckStats.breadDuckStats.limitMultiplier * (1 + MagicalDuckAmount * duckStats.magicalDuckStats.quackLimitMultiplier)));
            QuacksPerSecond = System.Math.Pow(SimpleDuckAmount * duckStats.simpleDuckStats.quacksPerSecond, BreadDuckBenefit);
            AddCurrency(QuacksPerSecond * Time.deltaTime);
            
            if (!_didShowSimpleDuck && SimpleDuckAmount > 0)
            {
                AfterSimpleDuck = QuacksPerSecond;
                _didShowSimpleDuck = true;
            }
            
            if (!_didShowBreadDuck && BreadDuckAmount > 0)
            {
                AfterBreadDuck = QuacksPerSecond;
                _didShowBreadDuck = true;
            }
            
            if (!_didShowMagicalDuck && MagicalDuckAmount > 0)
            {
                AfterMagicalDuck = QuacksPerSecond;
                _didShowMagicalDuck = true;
            }
            
            if (SteamManager.Initialized == false)
                return;

            if (!_didShowAchievement1 && CurrencyAmount >= 1001)
            {
                CurrencyAchievement("CURRENCY_1");
                _didShowAchievement1 = true;
            }
            if (!_didShowAchievement2 && CurrencyAmount >= 100000)
            {
                CurrencyAchievement("CURRENCY_2");
                _didShowAchievement2 = true;
            }
            
            if (!_didShowAchievement3 && CurrencyAmount >= 1000000000)
            {
                CurrencyAchievement("CURRENCY_3");
                _didShowAchievement3 = true;
            }
            
            if (!_didShowAchievement4 && CurrencyAmount >= 1e25)
            {
                CurrencyAchievement("CURRENCY_4");
                _didShowAchievement4 = true;
            }
        }

        private static void CurrencyAchievement(string achievement)
        {
            SteamUserStats.GetAchievement(achievement, out bool achieved);

            if (!achieved)
            {
                SteamUserStats.SetAchievement(achievement);
                SteamUserStats.StoreStats();
            }
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