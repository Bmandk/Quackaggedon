using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DuckClicker
{
    public class DuckFeeder : MonoBehaviour, ISaveable
    {
        public FoodType foodToThrow;
        public int breadPerThrow = 1;
        public bool selectedFromStart = false;

        public DuckFeederStats DuckFeederStats { get; private set; }
        private DuckType _duckTypeToSpawn;

        public long FoodThrown { get; private set; }
        private DuckSpawner _duckSpawner;
        public static DuckFeeder SelectedFeeder { get; private set; }
        private Button _button;
        public Slider progressSlider;
        public long NextDuckCost { get; private set; }
        [SerializeField] private bool useForAutoThrow;
        private float _autoThrowTimer;
        private float _autoBuyTimer;
        [SerializeField] private int _maxThrowParticles = 30;

        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private TMP_Text _foodPriceText;
        
        [SerializeField] private int _cheatDucksToSpawn = 0;
        [SerializeField] private bool _cheatSpawnDucks = false;
        [SerializeField] private bool _cheatThrowAllFood = false;

        [SerializeField] private Animator _animator;
        
        private List<SpawnDuckEvent> _spawnDuckEvents = new List<SpawnDuckEvent>();
        private int clicksSinceLastSpawn = 0;
        private int autoClicksSinceLastSpawn = 0;
        private int _parentIndex;
        [SerializeField] private GameObject _foodAmountHandPrefab;
        [SerializeField] private GameObject _foodAmountChefPrefab;
        private Canvas _canvas;
        [SerializeField] private Vector3 _foodAmountOffset;
        
        private void Awake()
        {
            _duckTypeToSpawn = DuckUnlockData.GetWhichDuckFoodUnlocks(foodToThrow);
            _button = GetComponent<Button>();
            _duckSpawner = FindObjectOfType<DuckSpawner>();

            _canvas = GetComponentInParent<Canvas>();
            var parent = transform.parent;
            _parentIndex = parent.GetSiblingIndex();
            parent.gameObject.SetActive(_parentIndex == 0);
        }

        private void Start()
        {
            DuckFeederStats = References.Instance.duckStats.GetDuckFeederStats(_duckTypeToSpawn);
            NextDuckCost = DuckFeederStats.CalculateCost(DuckAmounts.duckCounts[DuckType.Simple][AreaSettings.CurrentArea.AreaIndex]);
            _foodPriceText.text = $"{NumberUtility.FormatNumber(DuckFeederStats.foodCost)}";
            if (selectedFromStart)
            {
                Select();
            }
            
            UpdateProgress();
        }

        private void Update()
        {
            _button.interactable = CurrencyController.CanAfford(DuckFeederStats.foodCost);
            
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
                ThrowBread(false, false);
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


        public void ThrowBread(bool useCurrency, bool throwFromHand)
        {
            if (useCurrency)
            {
                clicksSinceLastSpawn++;
            }
            else
            {
                autoClicksSinceLastSpawn++;
            }
            
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
                actualFoodAmountThrown = Math.Min(attemptedFoodCountThisThrow, (long)CurrencyController.CurrencyAmount / DuckFeederStats.foodCost);
            
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_cheatThrowAllFood)
            {
                if (useCurrency)
                    actualFoodAmountThrown = (long)CurrencyController.CurrencyAmount / DuckFeederStats.foodCost;
                actualFoodAmountThrown = Math.Min(NextDuckCost - FoodThrown, actualFoodAmountThrown);
            }
#endif

            var costOfFood = actualFoodAmountThrown * DuckFeederStats.foodCost;
            if (useCurrency)
                CurrencyController.RemoveCurrency(costOfFood);

            int particles;
            
            if (actualFoodAmountThrown > _maxThrowParticles) 
                particles = _maxThrowParticles;
            else
                particles = (int)actualFoodAmountThrown;

            if (throwFromHand)
            {
                GameObject foodAmount = Instantiate(_foodAmountHandPrefab, transform.position + _foodAmountOffset, Quaternion.identity, _canvas.transform);
                ArmController.Instance.PerformFeedingHandAnimation(particles, foodToThrow);
                foodAmount.GetComponent<ClickDuckUiPopup>().SetFoodThrownOnClick(actualFoodAmountThrown, foodToThrow, costOfFood);

                //Handle player food stats across tracking across for whole playthrough
                PlayerFoodStats.AddTotalCostOfFoodThrownByHand(foodToThrow,costOfFood);
                PlayerFoodStats.AddHandThrownFood(foodToThrow, actualFoodAmountThrown);
            }
            else //in this case the particles will be spawned for the chef duck 
            {
                ThrowFoodParticles(particles, throwFromHand);

                GameObject foodAmount = Instantiate(_foodAmountChefPrefab, transform.position + _foodAmountOffset, Quaternion.identity, _canvas.transform);
                foodAmount.GetComponent<ClickDuckUiPopup>().SetFoodThrownByChef(actualFoodAmountThrown, foodToThrow);

                //Handle player food stats across tracking across for whole playthrough
                PlayerFoodStats.AddDuckThrownFood(foodToThrow, actualFoodAmountThrown);
            }
            PlayerFoodStats.AddToTotalFoodThrown(foodToThrow, actualFoodAmountThrown);

            FoodThrown += actualFoodAmountThrown;
            int ducksSpawned = 0;
            
            while (FoodThrown >= NextDuckCost) // _nextDuckCost is calculated in SpawnDuck
            {
                FoodThrown -= NextDuckCost;
                SpawnDuck(AreaSettings.CurrentArea);
                ducksSpawned++;
            }

            UpdateProgress();
            
            if (ducksSpawned > 0)
            {
                SaveManager.Save();
            }
        }

        public void ThrowFoodParticles(int particles, bool isFromHand)
        {
            Vector3 position = isFromHand ? ArmController.Instance.handPosition.position : DuckData.chefDucks[Random.Range(0, DuckData.chefDucks.Count)].transform.position;
            var foodPrefab = References.Instance.GetFoodData(foodToThrow).foodPrefab;
            var inst = Instantiate(foodPrefab, position, foodPrefab.transform.rotation, References.Instance.particleParent);
            inst.GetComponent<ParticleSystem>().Emit(particles);
        }

        private void SpawnDuck(AreaSettings area)
        {
            DuckData duckTypeSpawning = References.Instance.GetDuckData(_duckTypeToSpawn);
            if (duckTypeSpawning.duckType != DuckType.Muscle)
            {
                DuckAmounts.duckCounts[_duckTypeToSpawn][area.AreaIndex]++;
                _duckSpawner.SpawnDuck(duckTypeSpawning.duckPrefab, area);
                NextDuckCost = DuckFeederStats.CalculateCost(DuckAmounts.duckCounts[_duckTypeToSpawn][area.AreaIndex]);

                PlayFancyRevealIfFirstTimeSpawn();

                _spawnDuckEvents.Add(new SpawnDuckEvent
                {
                    clicksToSpawn = clicksSinceLastSpawn,
                    autoClicksToSpawn = autoClicksSinceLastSpawn,
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                });

                clicksSinceLastSpawn = 0;
                autoClicksSinceLastSpawn = 0;
            } else
            {
                EndStarter.Instance.StartEnd();
            }
        }

        private void PlayFancyRevealIfFirstTimeSpawn()
        {
            DuckData duckTypeSpawning = References.Instance.GetDuckData(_duckTypeToSpawn);
            if (DiscoveredObjects.DuckTypesSeen.Contains(duckTypeSpawning.duckType) || duckTypeSpawning.duckType == DuckType.Muscle)
            {
                return;
            }

            DiscoveredObjects.DuckTypesSeen.Add(duckTypeSpawning.duckType);
            References.Instance.duckopediaHandler.RefreshDuckopedia();
            StartCoroutine(RevealAfterDelay(1f, duckTypeSpawning));
        }

        IEnumerator RevealAfterDelay(float delay, DuckData duckData)
        {
            yield return new WaitForSeconds(delay);
            RevealNewUnlockedFoodButton();
            UIHandler.Instance.ShowRevealUI(duckData);
            AudioController.Instance.PlayRevealSounds();
        }

        private void RevealNewUnlockedFoodButton()
        {
            //Make sure to log that new food has now been discovered by player
            DiscoveredObjects.AddSeenFood(foodToThrow);
            DiscoveredObjects.AddSeenFood(DuckUnlockData.GetFoodUnlockedAfterThisOne(foodToThrow));
            
            transform.parent.parent.GetChild(_parentIndex + 1).gameObject.SetActive(true);
        }

        public void Select()
        {
            if (SelectedFeeder != null)
            {
                SelectedFeeder.Deselect();
            }
            
            SelectedFeeder = this;
            _button.interactable = false;
            
            NextDuckCost = DuckFeederStats.CalculateCost(DuckAmounts.duckCounts[_duckTypeToSpawn][AreaSettings.CurrentArea.AreaIndex]);
        }

        public void OnClick()
        {
            ThrowBread(true, true);
        }

        public void Refresh()
        {
            NextDuckCost = DuckFeederStats.CalculateCost(DuckAmounts.duckCounts[_duckTypeToSpawn][AreaSettings.CurrentArea.AreaIndex]);
            if (FoodThrown >= NextDuckCost)
            {
                long newFood = NextDuckCost - 1;
                FoodThrown = newFood;
            }

            UpdateProgress();
        }

        public void Deselect()
        {
            SelectedFeeder = null;
            _button.interactable = true;
        }
        
        private void UpdateProgress()
        {
            if (gameObject.activeInHierarchy == false)
                return;
            
            var newVal = (float)FoodThrown / NextDuckCost;
            if (newVal == 0)
            {
                FinishThisProgressLevel();
            }
            else
            {
                SetProgress(newVal);
            }

            _animator.SetTrigger("Pulse");
            _progressText.text = $"{NumberUtility.FormatNumber(FoodThrown)} / {NumberUtility.FormatNumber(NextDuckCost)}";
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
            {
                _progressLerp = StartCoroutine(LerpProgress());
            }
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

            _animator.SetTrigger("Fill");
            progressSlider.value = 0;
            _lerpingProgress = false;
        }

        public void Save(Dictionary<string, JToken> saveData)
        {
            var duckData = new Dictionary<string, JToken>
            {
                {"ducksSpawned", new JArray(DuckAmounts.duckCounts[_duckTypeToSpawn])},
                {"foodThrown", FoodThrown},
                {"spawnDuckEvents", JArray.FromObject(_spawnDuckEvents)},
                {"isRevealed", transform.parent.gameObject.activeSelf}
            };

            saveData[_duckTypeToSpawn.ToString()] = JObject.FromObject(duckData);
        }

        public void Load(Dictionary<string, JToken> saveData)
        {
            if (saveData.TryGetValue(_duckTypeToSpawn.ToString(), out JToken data))
            {
                Dictionary<string, JToken> duckFeederData = data.ToObject<Dictionary<string, JToken>>();
                FoodThrown = (int) duckFeederData["foodThrown"];

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
                
                if (duckFeederData.TryGetValue("spawnDuckEvents", out JToken throwFoodEventsData))
                {
                    _spawnDuckEvents = throwFoodEventsData.ToObject<List<SpawnDuckEvent>>();
                }
                
                if (duckFeederData.TryGetValue("isRevealed", out JToken isRevealed))
                {
                    transform.parent.gameObject.SetActive(isRevealed.ToObject<bool>());
                }
                
                UpdateProgress();
            }
        }
    }

    [Serializable]
    public struct SpawnDuckEvent
    {
        public int clicksToSpawn;
        public int autoClicksToSpawn;
        public string timestamp;
    }
}