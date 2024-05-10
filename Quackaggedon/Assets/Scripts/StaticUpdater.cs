using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DuckClicker
{
    public class StaticUpdater : MonoBehaviour
    {
        private void Awake()
        {
            CurrencyController.Reset();
            DiscoveredObjects.Reset();
            DuckFoodAmount.smartDuckCount = 0;
            DuckThrower.SetDuckAmount(0);
            DuckBuyer.SetDuckAmount(0);
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