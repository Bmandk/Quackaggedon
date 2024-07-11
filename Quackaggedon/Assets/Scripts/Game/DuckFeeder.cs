using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
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

        public double FoodThrown { get; private set; }
        private DuckSpawner _duckSpawner;
        public static DuckFeeder SelectedFeeder { get; private set; }
        private Button _button;

        [SerializeField]
        private Image _buttonIcon;
        [SerializeField]
        private Color colorHiddenIcon;

        //public Slider progressSlider;
        public double NextDuckCost { get; private set; }
        [SerializeField] private bool useForAutoThrow;
        private float _autoThrowTimer;
        private float _autoBuyTimer;
        [SerializeField] private int _maxThrowParticles = 30;

        public FoodSliderAnimator foodSliderAnimator;

        //[SerializeField] private TMP_Text _progressText;
        [SerializeField] private TMP_Text _foodPriceText;
        
        [SerializeField] private int _cheatDucksToSpawn = 0;
        [SerializeField] private bool _cheatSpawnDucks = false;
        [SerializeField] private bool _cheatThrowAllFood = false;

        //[SerializeField] private Animator _sliderAnimator;
        
        private int clicksSinceLastSpawn = 0;
        private int autoClicksSinceLastSpawn = 0;
        private int _parentIndex;
        [SerializeField] private GameObject _foodAmountHandPrefab;
        [SerializeField] private GameObject _foodAmountChefPrefab;
        private Canvas _canvas;
        [SerializeField] private Vector3 _foodAmountOffset;
        
        // Used for duckopedia details
        public static float ChefDuckTimer;
        public static float MagicalChefDuckBonus;
        public static double CleverDuckAmount;
        public static float MagicalCleverDuckBonus;
        
        // Used for popup reveals
        private static bool _didShowCleverDuck;
        private static bool _didShowChefDuck;
        
        private void Awake()
        {
            _duckTypeToSpawn = DuckUnlockData.GetWhichDuckFoodUnlocks(foodToThrow);
            _button = GetComponent<Button>();
            _duckSpawner = FindObjectOfType<DuckSpawner>();

            _canvas = GetComponentInParent<Canvas>();
            var parent = transform.parent;
            _parentIndex = parent.GetSiblingIndex();
            parent.gameObject.SetActive(_parentIndex == 0);

            DuckFeederStats = References.Instance.duckStats.GetDuckFeederStats(_duckTypeToSpawn);
            NextDuckCost = DuckFeederStats.CalculateCost(DuckAmounts.duckCounts[DuckType.Simple][AreaSettings.CurrentArea.AreaIndex]);
        }

        private void Start()
        {
            _foodPriceText.text = $"{NumberUtility.FormatNumber(DuckFeederStats.foodCost)}";
            if (selectedFromStart)
            {
                Select();
            }

            foodSliderAnimator.UpdateSliderProgress();
        }

        private void Update()
        {
            var CanAfford = CurrencyController.CanAfford(DuckFeederStats.foodCost);
            if (DiscoveredObjects.HasAffordedFood(foodToThrow))
            {
                _button.interactable = CanAfford;
            }
            else
            {
                if (CanAfford) 
                {
                    DiscoveredObjects.AddAffordedFood(foodToThrow);
                    _buttonIcon.sprite = References.Instance.GetFoodData(foodToThrow).foodIconRevealed;
                    _buttonIcon.color = Color.white;
                } 
                else
                {
                    _buttonIcon.sprite = References.Instance.GetFoodData(foodToThrow).foodIconHidden;
                    _buttonIcon.color = colorHiddenIcon;
                    _button.interactable = false;

                }
            }
            
            if (useForAutoThrow && DuckAmounts.duckCounts[DuckType.Chef][AreaSettings.CurrentArea.AreaIndex] > 0)
                CheckAutoBuyer();

            if (_cheatSpawnDucks)
            {
                _cheatSpawnDucks = false;
                for (int i = 0; i < _cheatDucksToSpawn; i++)
                {
                    SpawnDuck(AreaSettings.CurrentArea, false);
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
                ChefDuckTimer = _autoBuyTimer;
                magicalDucks = 0;
                MagicalChefDuckBonus = (float)(Math.Pow(chefDuckStats.timeGrowthRate, chefDucks + chefDuckStats.amountOffset + magicalDucks * References.Instance.duckStats.magicalDuckStats.chefMultiplier) + chefDuckStats.minTime) - _autoBuyTimer;
            }
            else
            {
                _autoBuyTimer -= Time.deltaTime;
            }
        }


        public void ThrowBread(bool useCurrency, bool throwFromHand)
        {
            if (throwFromHand)
            {
                References.Instance.sceneDataHolder.PulseAllCleverDucks();
            }

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
            double attemptedFoodCountThisThrow = Math.Max(
                Math.Pow(cleverDucks, References.Instance.duckStats.cleverDuckStats.minFoodPerDuck),
                Math.Pow(
                    References.Instance.duckStats.cleverDuckStats.foodAmountGrowthRate,
                    cleverDucks + magicDucks * References.Instance.duckStats.magicalDuckStats.cleverMultiplier)
                * References.Instance.duckStats.cleverDuckStats.foodAmountMultiplier) + 1;
            CleverDuckAmount = attemptedFoodCountThisThrow;
            
            // Don't overspend food
            attemptedFoodCountThisThrow = Math.Min(attemptedFoodCountThisThrow, NextDuckCost - FoodThrown); 
            double actualFoodAmountThrown = attemptedFoodCountThisThrow;
            if (useCurrency)
                actualFoodAmountThrown = Math.Min(attemptedFoodCountThisThrow, CurrencyController.CurrencyAmount / DuckFeederStats.foodCost);
            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_cheatThrowAllFood)
            {
                if (useCurrency)
                    actualFoodAmountThrown = CurrencyController.CurrencyAmount / DuckFeederStats.foodCost;
                actualFoodAmountThrown = Math.Min(NextDuckCost - FoodThrown, actualFoodAmountThrown);
            }
#endif
            
            actualFoodAmountThrown = Math.Floor(actualFoodAmountThrown);

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
                GameObject foodAmount = Instantiate(_foodAmountHandPrefab, transform.position + _foodAmountOffset, Quaternion.identity, References.Instance.menuController.incEffectParent);
                ArmController.Instance.PerformFeedingHandAnimation(particles, foodToThrow);
                foodAmount.GetComponent<ClickDuckUiPopup>().SetFoodThrownOnClick(actualFoodAmountThrown, foodToThrow, costOfFood);

                //Handle player food stats across tracking across for whole playthrough
                PlayerFoodStats.AddTotalCostOfFoodThrownByHand(foodToThrow, costOfFood);
                PlayerFoodStats.AddHandThrownFood(foodToThrow, actualFoodAmountThrown);
                UpdateStatsAndSpawnDuck(actualFoodAmountThrown);
            }
            else //in this case the particles will be spawned for the chef duck 
            {
                ChefServeFood(particles, actualFoodAmountThrown, throwFromHand);


            }
        }

        private void UpdateStatsAndSpawnDuck(double actualFoodAmountThrown)
        {
            PlayerFoodStats.AddToTotalFoodThrown(foodToThrow, actualFoodAmountThrown);
            FoodThrown += actualFoodAmountThrown;
            int ducksSpawned = 0;

            while (FoodThrown >= NextDuckCost) // _nextDuckCost is calculated in SpawnDuck
            {
                FoodThrown -= NextDuckCost;
                SpawnDuck(AreaSettings.CurrentArea, false);
                ducksSpawned++;
            }

            foodSliderAnimator.UpdateSliderProgress();

            if (ducksSpawned > 0)
            {
                SaveManager.Save();
            }
        }

        public void ChefServeFood(int particles, double actualFoodAmountThrown, bool isFromHand)
        {
            var randomChefPos = DuckData.duckObjects[DuckType.Chef][Random.Range(0, DuckData.duckObjects[DuckType.Chef].Count)].transform.position + new Vector3(0,1.2f,0);

            ChefPotHandler.Amount potLevel = particles < (_maxThrowParticles * 0.4f) ? ChefPotHandler.Amount.LittleFood : particles < (_maxThrowParticles * 0.75f) ? ChefPotHandler.Amount.MediumFood : ChefPotHandler.Amount.MuchFood;

            References.Instance.sceneDataHolder.MakeAllChefsSendIngredientToCookingChef(randomChefPos,foodToThrow);

            StartCoroutine(WaitSomeThenPot(potLevel,randomChefPos,actualFoodAmountThrown)); 


        }

        IEnumerator WaitSomeThenPot(ChefPotHandler.Amount potLevel, Vector3 randomChefPos, double actualFoodAmountThrown)
        {
            yield return new WaitForSeconds(0.4f);

            References.Instance.chefPotHandler.ShowChefPot(foodToThrow, potLevel, randomChefPos);

            GameObject foodAmount = Instantiate(_foodAmountChefPrefab, transform.position + _foodAmountOffset, Quaternion.identity,References.Instance.menuController.incEffectParent);
            foodAmount.GetComponent<ClickDuckUiPopup>().SetFoodThrownByChef(actualFoodAmountThrown, foodToThrow);
            foodAmount.transform.position = References.Instance.mainCam.WorldToScreenPoint(randomChefPos + new Vector3(0, 0.2f, 0));

            //Handle player food stats across tracking across for whole playthrough
            PlayerFoodStats.AddDuckThrownFood(foodToThrow, actualFoodAmountThrown);
            UpdateStatsAndSpawnDuck(actualFoodAmountThrown);


        }


        private void SpawnDuck(AreaSettings area, bool loadingFromSave)
        {
            DuckData duckTypeSpawning = References.Instance.GetDuckData(_duckTypeToSpawn);
            if (duckTypeSpawning.duckType != DuckType.Muscle)
            {
                DuckAmounts.duckCounts[_duckTypeToSpawn][area.AreaIndex]++;

                GameObject spawnedDuckData = _duckSpawner.SpawnDuck(duckTypeSpawning.duckPrefab, area, loadingFromSave);
                if (duckTypeSpawning.duckType == DuckType.Simple)
                {
                    spawnedDuckData.GetComponent<SimpleDuckEquipment>().EnableCorrectVisualOnSpawn();
                }

                References.Instance.sceneDataHolder.StoreDuckData(spawnedDuckData);

                NextDuckCost = DuckFeederStats.CalculateCost(DuckAmounts.duckCounts[_duckTypeToSpawn][area.AreaIndex]);

                PlayFancyRevealIfFirstTimeSpawn();

                clicksSinceLastSpawn = 0;
                autoClicksSinceLastSpawn = 0;
            } else
            {
                EndStarter.hasPlayerFinishedGame = true;
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

            RevealHandler.Instance.AddActionToAfterReveal(() => RevealNewUnlockedFoodButton());
            RevealHandler.Instance.ShowRevealUI(duckData);

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
            if (_duckTypeToSpawn == DuckType.Simple && DuckAmounts.duckCounts[DuckType.Simple][AreaSettings.CurrentArea.AreaIndex] == 0)
            {
                TutorialController.HideTutorialArrow();
            }
            
            ThrowBread(true, true);
        }

        public void Refresh()
        {
            NextDuckCost = DuckFeederStats.CalculateCost(DuckAmounts.duckCounts[_duckTypeToSpawn][AreaSettings.CurrentArea.AreaIndex]);
            if (FoodThrown >= NextDuckCost)
            {
                double newFood = NextDuckCost - 1;
                FoodThrown = newFood;
            }

            foodSliderAnimator.UpdateSliderProgress();
        }

        public void Deselect()
        {
            SelectedFeeder = null;
            _button.interactable = true;
        }
        

        public void Save(Dictionary<string, JToken> saveData)
        {
            var duckData = new Dictionary<string, JToken>
            {
                {"ducksSpawned", new JArray(DuckAmounts.duckCounts[_duckTypeToSpawn])},
                {"ducksInHut", DuckAmounts.hutAmounts[_duckTypeToSpawn]},
                {"foodThrown", FoodThrown},
                {"isRevealed", transform.parent.gameObject.activeSelf}
            };

            saveData[_duckTypeToSpawn.ToString()] = JObject.FromObject(duckData);
        }

        public void Load(Dictionary<string, JToken> saveData)
        {
            if (saveData.TryGetValue(_duckTypeToSpawn.ToString(), out JToken data))
            {
                Dictionary<string, JToken> duckFeederData = data.ToObject<Dictionary<string, JToken>>();
                FoodThrown = (double) duckFeederData["foodThrown"];

                List<AreaSettings> areaSettings = FindObjectsOfType<AreaSettings>().ToList();
                
                if (duckFeederData.TryGetValue("ducksInHut", out JToken ducksInHutData))
                {
                    DuckAmounts.hutAmounts[_duckTypeToSpawn] = ducksInHutData.ToObject<long>();
                }
                
                if (duckFeederData.TryGetValue("ducksSpawned", out JToken ducksSpawnedData))
                {
                    long[] ducksSpawnedDataArray = ducksSpawnedData.ToObject<long[]>();
                    for (int i = 0; i < areaSettings.Count; i++)
                    {
                        long ducksSpawnedCount = ducksSpawnedDataArray.Length > i ? ducksSpawnedDataArray[areaSettings[i].AreaIndex] : 0;
                        for (int j = 0; j < ducksSpawnedCount - DuckAmounts.hutAmounts[_duckTypeToSpawn]; j++)
                        {
                            SpawnDuck(areaSettings[i], true);
                        }
                    }
                    DuckAmounts.duckCounts[_duckTypeToSpawn] = ducksSpawnedDataArray;
                }
                
                if (duckFeederData.TryGetValue("isRevealed", out JToken isRevealed))
                {
                    transform.parent.gameObject.SetActive(isRevealed.ToObject<bool>());
                }

                foodSliderAnimator.UpdateSliderProgress();
            }
        }
    }
}