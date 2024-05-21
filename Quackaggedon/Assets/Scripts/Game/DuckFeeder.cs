using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
        public Slider progressSlider;
        private int _nextDuckCost = 0;
        [SerializeField] private bool useForAutoThrow;
        private float _autoThrowTimer;
        private float _autoBuyTimer;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _foodText = GetComponentInChildren<TMP_Text>();
            _duckSpawner = FindObjectOfType<DuckSpawner>();
        }

        private void Start()
        {
            _nextDuckCost = duckCost.CalculateCost(DuckAmounts.duckCounts[DuckType.Simple][AreaSettings.CurrentArea.AreaIndex]);
            if (selectedFromStart)
            {
                Select();
            }
        }

        private void Update()
        {
            _foodText.text = $"{foodAmount}";
            if (useForAutoThrow && DuckAmounts.duckCounts[DuckType.LunchLady][AreaSettings.CurrentArea.AreaIndex] > 0)
                CheckAutoThrower();
            
            if (useForAutoThrow && DuckAmounts.duckCounts[DuckType.Chef][AreaSettings.CurrentArea.AreaIndex] > 0)
                CheckAutoBuyer();
        }

        private void CheckAutoThrower()
        {
            if (_autoThrowTimer <= 0)
            {
                ThrowBread();
                _autoThrowTimer = 1f / Mathf.Sqrt(DuckAmounts.GetTotalDucks(DuckType.LunchLady));
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
                _autoBuyTimer = 1f / Mathf.Sqrt(DuckAmounts.GetTotalDucks(DuckType.Chef));
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

            int breadThisThrow = Mathf.Min(foodAmount, Mathf.Max(1, DuckAmounts.GetTotalDucks(DuckType.Clever) *
                (DuckAmounts.GetTotalDucks(DuckType.Magical) + 1) + 1));
            foodAmount -= breadThisThrow;

            var foodPrefab = References.Instance.GetFoodData(foodToThrow).foodPrefab;
            var inst = Instantiate(foodPrefab);
            inst.GetComponent<ParticleSystem>().Emit(breadThisThrow);

            //breadParticles.Emit(breadThisThrow);
            
            _foodThrown += breadThisThrow;
            
            while (_foodThrown >= _nextDuckCost)
            {
                _foodThrown -= _nextDuckCost;
                SpawnDuck(AreaSettings.CurrentArea);
            }
            
            UpdateProgress();
        }

        private void SpawnDuck(AreaSettings area)
        {
            DuckData duckTypeSpawning = References.Instance.GetDuckData(duckTypeToSpawn);
            DuckAmounts.duckCounts[duckTypeToSpawn][area.AreaIndex]++;
            _duckSpawner.SpawnDuck(duckTypeSpawning.duckPrefab, area);
            _nextDuckCost = duckCost.CalculateCost(DuckAmounts.duckCounts[duckTypeToSpawn][area.AreaIndex]);

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
            
            _nextDuckCost = duckCost.CalculateCost(DuckAmounts.duckCounts[duckTypeToSpawn][AreaSettings.CurrentArea.AreaIndex]);
        }

        public void Refresh()
        {
            _nextDuckCost = duckCost.CalculateCost(DuckAmounts.duckCounts[duckTypeToSpawn][AreaSettings.CurrentArea.AreaIndex]);
            if (_foodThrown >= _nextDuckCost)
            {
                int newFood = _nextDuckCost - 1;
                foodAmount += _foodThrown - newFood; 
                _foodThrown = newFood;

            }
            UpdateProgress();
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
        
        private void UpdateProgress()
        {
            var newVal = (float)_foodThrown / _nextDuckCost;
            if (newVal == 0)
            {
                FinishThisProgressLevel();
            }
            else
            {

                SetProgress(newVal);
            } 
        }

        private float _targetProgress;
        private float _timeScale = 0;
        private bool _lerpingProgress = false;
        private float _fillSpeed = 3;
        private Coroutine _progressLerp;

        public void FinishThisProgressLevel()
        {
            _timeScale = 0;

            if (_progressLerp != null)
            {
                StopCoroutine(_progressLerp);
                _lerpingProgress = false;
            }
            if (!_lerpingProgress)
                _progressLerp = StartCoroutine(LerpProgressToFinish());
        }

        public void SetProgress(float progress)
        {
            _targetProgress = progress;
            _timeScale = 0;

            if (_progressLerp != null)
            {
                StopCoroutine(_progressLerp);
                _lerpingProgress = false;
            }
            if (!_lerpingProgress)
                _progressLerp = StartCoroutine(LerpProgress());
        }

        private IEnumerator LerpProgress()
        {
            float startHealth = progressSlider.value;

            _lerpingProgress = true;

            while (_timeScale < 1)
            {
                _timeScale += Time.deltaTime * _fillSpeed;
                progressSlider.value = Mathf.Lerp(startHealth, _targetProgress, _timeScale);
                yield return null;
            }
            _lerpingProgress = false;
        }

        private IEnumerator LerpProgressToFinish()
        {
            float startHealth = progressSlider.value;

            _lerpingProgress = true;

            while (_timeScale < 1)
            {
                _timeScale += Time.deltaTime * _fillSpeed;
                progressSlider.value = Mathf.Lerp(startHealth, 1, _timeScale);
                yield return null;
            }
            progressSlider.value = 0;
            _lerpingProgress = false;
        }

        public void Save(Dictionary<string, JToken> saveData)
        {
            var duckData = new Dictionary<string, JToken>
            {
                {"foodAmount", foodAmount},
                {"ducksSpawned", new JArray(DuckAmounts.duckCounts[duckTypeToSpawn])},
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
                
                UpdateProgress();
            }
        }
    }
}