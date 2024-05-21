using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckClicker
{
    public class StaticUpdater : MonoBehaviour
    {
        private void Awake()
        {
            DuckAmounts.Reset();
            CurrencyController.Reset();
            DiscoveredObjects.Reset();
        }

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("DuckClickerScene"));
            SaveManager.Load();
        }

        private void Update()
        {
            CurrencyController.Update();
        }

        private void OnApplicationQuit()
        {
            SaveManager.Save();
        }
    }
}