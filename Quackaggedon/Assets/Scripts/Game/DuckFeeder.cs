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
        public FoodType foodToThrow;
        public int breadPerThrow = 1;
        public bool selectedFromStart = false;

        private DuckFeederStats _duckFeederStats;
        public DuckType duckTypeToSpawn;

        private long _foodThrown = 0;
        private DuckSpawner _duckSpawner;
        public static DuckFeeder SelectedFeeder { get; private set; }
        private Button _button;
        private TMP_Text _foodText;
        public Slider progressSlider;
        private long _nextDuckCost = 0;
        [SerializeField] private bool useForAutoThrow;
        private float _autoThrowTimer;
        private float _autoBuyTimer;
        [SerializeField] private int _maxThrowParticles = 30;

        [SerializeField] private TMP_Text _progressText;
        
        [SerializeField] private int _cheatDucksToSpawn = 0;
        [SerializeField] private bool _cheatSpawnDucks = false;
        [SerializeField] private bool _cheatThrowAllFood = false;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _foodText = GetComponentInChildren<TMP_Text>();
            _duckSpawner = FindObjectOfType<DuckSpawner>();
        }

        private void Start()
        {
            _duckFeederStats = References.Instance.duckStats.GetDuckFeederStats(duckTypeToSpawn);
            _nextDuckCost = _duckFeederStats.CalculateCost(DuckAmounts.duckCounts[DuckType.Simple][AreaSettings.CurrentArea.AreaIndex]);
            _foodText.text = $"{_duckFeederStats.foodCost}";
            if (selectedFromStart)
            {
                Select();
            }
        }

        private void Update()
        {
            _button.interactable = CurrencyController.CanAfford(_duckFeederStats.foodCost);
            
            if (useForAutoThrow && DuckAmounts.duckCounts[DuckType.Chef][AreaSettings.CurrentArea.AreaIndex] > 0)
                CheckAutoBuyer();

            if (_cheatSpawnDucks)
            {
                _cheatSpawnDucks = false;
                for (int i = 0; i < _cheatDucksToSpawn; i++)
                {
                    SpawnDuck(AreaSettings.CurrentArea);
                }
            }
        }

        private void CheckAutoBuyer()
        {
            if (_autoBuyTimer <= 0)
            {
                ThrowBread(false);
                // POW($I$6,Z11-$J$6+$K$6*P11)+$L$6
                long chefDucks = DuckAmounts.GetTotalDucks(DuckType.Chef);
                long magicalDucks = DuckAmounts.GetTotalDucks(DuckType.Magical);
                ChefDuckStats chefDuckStats = References.Instance.duckStats.chefDuckStats;
                _autoBuyTimer = (float)(Math.Pow(chefDuckStats.timeGrowthRate, chefDucks + chefDuckStats.amountOffset + magicalDucks * References.Instance.duckStats.magicalDuckStats.chefMultiplier) + chefDuckStats.minTime);
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

        public void ThrowBread(bool useCurrency)
        {
            // =FLOOR(MAX(POW(S11,$L$6), POW($J$6,S11+$K$6*P11)*$I$6)+1)
            long cleverDucks = DuckAmounts.GetTotalDucks(DuckType.Clever);
            long magicDucks = DuckAmounts.GetTotalDucks(DuckType.Magical);
            long attemptedFoodCountThisThrow = (long)Math.Max(
                Math.Pow(cleverDucks, References.Instance.duckStats.cleverDuckStats.minFoodPerDuck),
                Math.Pow(
                    References.Instance.duckStats.cleverDuckStats.foodAmountGrowthRate,
                    cleverDucks + magicDucks * References.Instance.duckStats.magicalDuckStats.cleverMultiplier)
                * References.Instance.duckStats.cleverDuckStats.foodAmountMultiplier) + 1;
            long actualFoodAmountThrown = attemptedFoodCountThisThrow;
            if (useCurrency)
                actualFoodAmountThrown = Math.Min(attemptedFoodCountThisThrow, (long)CurrencyController.CurrencyAmount / _duckFeederStats.foodCost);
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_cheatThrowAllFood)
            {
                if (useCurrency)
                    actualFoodAmountThrown = (long)CurrencyController.CurrencyAmount / _duckFeederStats.foodCost;
                actualFoodAmountThrown = Math.Min(_nextDuckCost - _foodThrown, actualFoodAmountThrown);
            }
            #endif
            
            if (useCurrency)
                CurrencyController.RemoveCurrency(actualFoodAmountThrown * _duckFeederStats.foodCost);
            
            var foodPrefab = References.Instance.GetFoodData(foodToThrow).foodPrefab;
            var inst = Instantiate(foodPrefab);
            int particles;
            if (actualFoodAmountThrown > _maxThrowParticles)
                particles = _maxThrowParticles;
            else
                particles = (int) actualFoodAmountThrown;
            inst.GetComponent<ParticleSystem>().Emit(particles);

            //breadParticles.Emit(breadThisThrow);
            
            _foodThrown += actualFoodAmountThrown;
            
            while (_foodThrown >= _nextDuckCost) // _nextDuckCost is calculated in SpawnDuck
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
            _nextDuckCost = _duckFeederStats.CalculateCost(DuckAmounts.duckCounts[duckTypeToSpawn][area.AreaIndex]);

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
                References.Instance.duckopediaHandler.RefreshDuckopedia();
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
            
            _nextDuckCost = _duckFeederStats.CalculateCost(DuckAmounts.duckCounts[duckTypeToSpawn][AreaSettings.CurrentArea.AreaIndex]);
        }

        public void OnClick()
        {
            ThrowBread(true);
        }

        public void Refresh()
        {
            _nextDuckCost = _duckFeederStats.CalculateCost(DuckAmounts.duckCounts[duckTypeToSpawn][AreaSettings.CurrentArea.AreaIndex]);
            if (_foodThrown >= _nextDuckCost)
            {
                long newFood = _nextDuckCost - 1;
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
        
        /*public void BuyFood()
        {
            if (CurrencyController.CanAfford(_duckFeederStats.foodCost))
            {
                CurrencyController.RemoveCurrency(_duckFeederStats.foodCost);
                foodAmount++;
            }
        }*/
        
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
            
            _progressText.text = $"{NumberUtility.FormatNumber(_foodThrown)} / {NumberUtility.FormatNumber(_nextDuckCost)}";
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
                {"ducksSpawned", new JArray(DuckAmounts.duckCounts[duckTypeToSpawn])},
                {"foodThrown", _foodThrown}
            };

            saveData[References.Instance.GetDuckData(duckTypeToSpawn).duckPrefab.name] = JObject.FromObject(duckData);
        }

        public void Load(Dictionary<string, JToken> saveData)
        {
            if (gameObject.activeInHierarchy == false)
            {
                return;
            }
            
            if (saveData.TryGetValue(References.Instance.GetDuckData(duckTypeToSpawn).duckPrefab.name, out JToken data))
            {
                Dictionary<string, JToken> duckFeederData = data.ToObject<Dictionary<string, JToken>>();
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