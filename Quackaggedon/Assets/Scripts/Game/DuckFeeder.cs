using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DuckClicker
{
    public class DuckFeeder : MonoBehaviour
    {
        public Animator arm;
        public ParticleSystem breadParticles;
        public int breadPerThrow = 1;
        public int foodAmount = 10;
        public float foodUsePerSecond = 3.0f;
        public int foodPerDuck = 10;
        public float foodCost = 10f;
        public bool selectedFromStart = false;
        public GameObject duckPrefab;        
        private bool _isFeeding = false;
        private int _foodUntilNextDuck = 0;
        private DuckSpawner _duckSpawner;
        public static DuckFeeder SelectedFeeder { get; private set; }
        private Button _button;
        private TMP_Text _foodText;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _foodText = GetComponentInChildren<TMP_Text>();
            if (selectedFromStart)
            {
                Select();
            }
        }

        private void Start()
        {
            _duckSpawner = FindObjectOfType<DuckSpawner>();
        }

        private void Update()
        {
            _foodText.text = $"Select Food {transform.GetSiblingIndex() + 1}\nAmount: {foodAmount}";
        }

        public void ToggleFeeding(bool isFeeding)
        {
            _isFeeding = isFeeding;
            arm.SetBool("Throwing", isFeeding);
        }

        public void ThrowBread()
        {
            if (foodAmount <= 0)
            {
                return;
            }
            
            int breadThisThrow = Mathf.Min(foodAmount, breadPerThrow);
            foodAmount -= breadThisThrow;
            breadParticles.Emit(breadThisThrow);
            
            _foodUntilNextDuck += breadThisThrow;
            
            if (_foodUntilNextDuck >= foodPerDuck)
            {
                _foodUntilNextDuck -= foodPerDuck;
                _duckSpawner.SpawnDuck(duckPrefab);
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
        }
        
        public void Deselect()
        {
            SelectedFeeder = null;
            _button.interactable = true;
            ToggleFeeding(false);
        }
    }
}