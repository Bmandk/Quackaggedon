using System;
using UnityEngine;

namespace DuckClicker
{
    public class StaticUpdater : MonoBehaviour
    {
        private void Awake()
        {
            CurrencyController.Reset();
        }

        private void Update()
        {
            
        }
    }
}