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
            PlayerFoodStats.Reset();
            DuckData.chefDucks = new System.Collections.Generic.List<Transform>();
        }

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("DuckClickerScene"));
            bool loadedSave = SaveManager.Load();
            if (loadedSave) 
            {
                CleanPoolHandler.Instance.DisableDirt();
            }
            else
            {
                CleanPoolHandler.Instance.EnableDirt();
            }
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