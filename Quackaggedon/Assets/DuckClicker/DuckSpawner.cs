using UnityEngine;

namespace DuckClicker
{
    public class DuckSpawner : MonoBehaviour
    {
        public GameObject duckPrefab;
        public Transform[] spawnPoints;
        public float spawnRadius = 1.0f;
        
        public float spawnInterval = 0.3f;
        private float spawnTimer = 0.0f;
        public bool isSpawning = false;
        
        public float quacksPerDuckPerSecond = 1.0f;

        private float ducksSpawned;
        private float quacks;
        
        public void StartSpawn()
        {
            isSpawning = true;
            spawnTimer = 0.0f;
            SpawnDuck();
        }
        
        public void StopSpawn()
        {
            isSpawning = false;
        }
        
        void Update()
        {
            CheckSpawn();
            IncreaseQuacks();
        }

        private void CheckSpawn()
        {
            if (isSpawning)
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= spawnInterval)
                {
                    spawnTimer = 0.0f;
                    SpawnDuck();
                }
            }
        }

        public void SpawnDuck()
        {
            GameObject duck = GameObject.Instantiate(duckPrefab);
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            duck.transform.position = spawnPoint + Random.insideUnitSphere * spawnRadius;
            ducksSpawned++;
        }
        
        private void IncreaseQuacks()
        {
            quacks += ducksSpawned * quacksPerDuckPerSecond * Time.deltaTime;
        }
    }
}