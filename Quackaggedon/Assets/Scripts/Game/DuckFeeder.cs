using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DuckClicker
{
    public class DuckFeeder : MonoBehaviour
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
        public Animator arm;
        public ParticleSystem breadParticles;
        public int breadPerThrow = 1;
        public int foodAmount = 10;
        public DuckCost duckCost;
        public float foodCost = 10f;
        public bool selectedFromStart = false;
        public GameObject duckPrefab;
        private int _foodThrown = 0;
        private DuckSpawner _duckSpawner;
        public static DuckFeeder SelectedFeeder { get; private set; }
        private Button _button;
        private TMP_Text _foodText;
        private int[] ducksSpawned;
        private int _nextDuckCost = 0;

        private void Awake()
        {
            ducksSpawned = new int[3];
            _button = GetComponent<Button>();
            _foodText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _duckSpawner = FindObjectOfType<DuckSpawner>();
            _nextDuckCost = duckCost.CalculateCost(ducksSpawned[AreaSettings.CurrentArea.AreaIndex]);
            if (selectedFromStart)
            {
                Select();
            }
        }

        private void Update()
        {
            _foodText.text = $"{foodAmount}";
        }

        public void PerformFeedingHandAnimation()
        {
            //arm.SetBool("Throwing", isFeeding);
            arm.SetTrigger("Throwing");
        }

        public void ThrowBread()
        {
            if (foodAmount <= 0 || !AreaSettings.CurrentArea.CanSpawnDuck)
            {
                return;
            }

            Debug.Log(DuckSmart.smartDuckCount);
            int breadThisThrow = Mathf.Min(foodAmount, Mathf.Max(1, DuckSmart.smartDuckCount + 1));
            foodAmount -= breadThisThrow;
            breadParticles.Emit(breadThisThrow);
            
            _foodThrown += breadThisThrow;
            
            while (_foodThrown >= _nextDuckCost)
            {
                ducksSpawned[AreaSettings.CurrentArea.AreaIndex]++;
                _duckSpawner.SpawnDuck(duckPrefab);
                _foodThrown -= _nextDuckCost;
                _nextDuckCost = duckCost.CalculateCost(ducksSpawned[AreaSettings.CurrentArea.AreaIndex]);
            }
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
    }
}