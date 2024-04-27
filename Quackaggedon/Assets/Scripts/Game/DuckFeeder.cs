using System;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        private bool _isFeeding = false;
        private int _foodUntilNextDuck = 0;
        private DuckSpawner _duckSpawner;

        private void Start()
        {
            _duckSpawner = FindObjectOfType<DuckSpawner>();
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
                _duckSpawner.SpawnDuck();
            }
        }
    }
}