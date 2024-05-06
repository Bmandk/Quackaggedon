﻿using System;
using UnityEngine;

namespace DuckClicker
{
    public class StaticUpdater : MonoBehaviour
    {
        private void Awake()
        {
            CurrencyController.Reset();
            DuckSmart.smartDuckCount = 0;
        }

        private void Update()
        {
            CurrencyController.Update();
        }
    }
}