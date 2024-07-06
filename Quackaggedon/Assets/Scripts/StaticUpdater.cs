using System.Collections.Generic;
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
            TutorialController.Reset();
            HutRevealController.ResetHutValues();
            DuckData.duckObjects = new Dictionary<DuckType, List<DuckData>>
            {
                { DuckType.Simple, new List<DuckData>() },
                { DuckType.Clever, new List<DuckData>() },
                { DuckType.Bread, new List<DuckData>() },
                { DuckType.LunchLady, new List<DuckData>() },
                { DuckType.Chef, new List<DuckData>() },
                { DuckType.Magical, new List<DuckData>() },
                { DuckType.Muscle, new List<DuckData>() }
            };
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