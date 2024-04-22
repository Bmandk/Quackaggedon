using System;
using UnityEngine;

namespace DuckClicker
{
    public class DuckFeeder : MonoBehaviour
    {
        public Animator arm;
        public ParticleSystem breadParticles;
        public int breadCount = 30;
        private DuckSpawner spawner;

        private void Awake()
        {
            spawner = FindObjectOfType<DuckSpawner>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ToggleFeeding(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                ToggleFeeding(false);
            }
        }

        public void ToggleFeeding(bool isFeeding)
        {
            arm.SetBool("Throwing", isFeeding);
            if (isFeeding)
            {
                spawner.StartSpawn();
            }
            else
            {
                spawner.StopSpawn();
            }
        }

        public void ThrowBread()
        {
            breadParticles.Emit(breadCount);
        }
    }
}