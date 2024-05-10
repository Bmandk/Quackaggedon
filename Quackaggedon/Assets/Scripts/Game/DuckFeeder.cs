using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DuckClicker
{
    public class DuckFeeder : MonoBehaviour, ISaveable
    {
        [Serializable]
        public struct DuckCost
        {
            public float baseFoodPerDuck;
            public float growthRate;
            
            public int CalculateCost(int ducksSpawned)
            {
                return Mathf.RoundToInt(baseFoodPerDuck * Mathf.Pow(growthRate, ducksSpawned));
            }
        }

        public FoodType foodToThrow;
        public int breadPerThrow = 1;
        public int foodAmount = 10;
        public float foodCost = 10f;
        public bool selectedFromStart = false;

        public DuckType duckTypeToSpawn;
        public DuckCost duckCost;

        private int _foodThrown = 0;
        private DuckSpawner _duckSpawner;
        public static DuckFeeder SelectedFeeder { get; private set; }
        private Button _button;
        private TMP_Text _foodText;
        private int[] ducksSpawned;
        private int _nextDuckCost = 0;
        [SerializeField] private bool useForAutoThrow;
        private float _autoThrowTimer;
        private float _autoBuyTimer;

        private void Awake()
        {
            ducksSpawned = new int[3];
            _button = GetComponent<Button>();
            _foodText = GetComponentInChildren<TMP_Text>();
            _duckSpawner = FindObjectOfType<DuckSpawner>();
        }

        private void Start()
        {
            _nextDuckCost = duckCost.CalculateCost(ducksSpawned[AreaSettings.CurrentArea.AreaIndex]);
            if (selectedFromStart)
            {
                Select();
            }
        }

        private void Update()
        {
            _foodText.text = $"{foodAmount}";
            if (useForAutoThrow && DuckThrower.speed > 0)
                CheckAutoThrower();
            
            if (useForAutoThrow && DuckBuyer.speed > 0)
                CheckAutoBuyer();
        }

        private void CheckAutoThrower()
        {
            if (_autoThrowTimer <= 0)
            {
                ThrowBread();
                _autoThrowTimer = 1f / (DuckThrower.speed * (DuckBonus.AmountOfDucks + 1));
            }
            else
            {
                _autoThrowTimer -= Time.deltaTime;
            }
        }
        
        private void CheckAutoBuyer()
        {
            if (_autoBuyTimer <= 0)
            {
                BuyFood();
                _autoBuyTimer = 1f / (DuckBuyer.speed * (DuckBonus.AmountOfDucks + 1));
            }
            else
            {
                _autoBuyTimer -= Time.deltaTime;
            }
        }

        public void PerformFeedingHandAnimation()
        {
            ArmController.Instance.PerformFeedingHandAnimation();
        }

        public void ThrowBread()
        {
            if (foodAmount <= 0 || !AreaSettings.CurrentArea.CanSpawnDuck)
            {
                return;
            }

            int breadThisThrow = Mathf.Min(foodAmount, Mathf.Max(1, DuckFoodAmount.smartDuckCount *
                (DuckBonus.AmountOfDucks + 1) + 1));
            foodAmount -= breadThisThrow;

            var foodPrefab = References.Instance.GetFoodData(foodToThrow).foodPrefab;
            var inst = Instantiate(foodPrefab);
            inst.GetComponent<ParticleSystem>().Emit(breadThisThrow);

            //breadParticles.Emit(breadThisThrow);
            
            _foodThrown += breadThisThrow;
            
            while (_foodThrown >= _nextDuckCost)
            {
                SpawnDuck(AreaSettings.CurrentArea);
                _foodThrown -= _nextDuckCost;
            }
        }

        private void SpawnDuck(AreaSettings area)
        {
            DuckData duckTypeSpawning = References.Instance.GetDuckData(duckTypeToSpawn);
            ducksSpawned[AreaSettings.CurrentArea.AreaIndex]++;
            _duckSpawner.SpawnDuck(duckTypeSpawning.duckPrefab, area);
            _nextDuckCost = duckCost.CalculateCost(ducksSpawned[AreaSettings.CurrentArea.AreaIndex]);

            PlayFancyRevealIfFirstTimeSpawn();
        }

        private void PlayFancyRevealIfFirstTimeSpawn()
        {
            DuckData duckTypeSpawning = References.Instance.GetDuckData(duckTypeToSpawn);
            if (DiscoveredObjects.DuckTypesSeen.Contains(duckTypeSpawning.duckType))
            {
                return;
            }
            else
            {
                DiscoveredObjects.DuckTypesSeen.Add(duckTypeSpawning.duckType);
                StartCoroutine(RevealAfterDelay(1f, duckTypeSpawning));
            }
        }

        IEnumerator RevealAfterDelay(float delay, DuckData duckData)
        {
            yield return new WaitForSeconds(delay);
            UIHandler.Instance.ShowRevealUI(duckData);
            AudioController.Instance.PlayRevealSounds();
        }

        public void Select()
        {
            if (SelectedFeeder != null)
            {
                SelectedFeeder.Deselect();
            }
            
            SelectedFeeder = this;
            _button.interactable = false;
            
            _nextDuckCost = duckCost.CalculateCost(ducksSpawned[AreaSettings.CurrentArea.AreaIndex]);
        }

        public void Refresh()
        {
            _nextDuckCost = duckCost.CalculateCost(ducksSpawned[AreaSettings.CurrentArea.AreaIndex]);
        }
        
        public void Deselect()
        {
            SelectedFeeder = null;
            _button.interactable = true;
            //PerformFeedingHandAnimation(false);
        }
        
        public void BuyFood()
        {
            if (CurrencyController.CanAfford(foodCost))
            {
                CurrencyController.RemoveCurrency(foodCost);
                foodAmount++;
            }
        }

        public void Save(Dictionary<string, JToken> saveData)
        {
            var duckData = new Dictionary<string, JToken>
            {
                {"foodAmount", foodAmount},
                {"ducksSpawned", new JArray(ducksSpawned)},
                {"foodThrown", _foodThrown}
            };

            saveData[References.Instance.GetDuckData(duckTypeToSpawn).duckPrefab.name] = JObject.FromObject(duckData);
        }

        public void Load(Dictionary<string, JToken> saveData)
        {
            if (saveData.TryGetValue(References.Instance.GetDuckData(duckTypeToSpawn).duckPrefab.name, out JToken data))
            {
                Dictionary<string, JToken> duckFeederData = data.ToObject<Dictionary<string, JToken>>();
                foodAmount = (int) duckFeederData["foodAmount"];
                _foodThrown = (int) duckFeederData["foodThrown"];

                List<AreaSettings> areaSettings = FindObjectsOfType<AreaSettings>().ToList();
                
                if (duckFeederData.TryGetValue("ducksSpawned", out JToken ducksSpawnedData))
                {
                    int[] ducksSpawnedDataArray = ducksSpawnedData.ToObject<int[]>();
                    for (int i = 0; i < areaSettings.Count; i++)
                    {
                        int ducksSpawnedCount = ducksSpawnedDataArray.Length > i ? ducksSpawnedDataArray[areaSettings[i].AreaIndex] : 0;
                        for (int j = 0; j < ducksSpawnedCount; j++)
                        {
                            SpawnDuck(areaSettings[i]);
                        }
                    }
                }
            }
        }
    }
}