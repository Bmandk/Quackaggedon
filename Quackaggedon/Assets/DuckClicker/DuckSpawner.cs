using UnityEngine;

namespace DuckClicker
{
    public class DuckSpawner : MonoBehaviour
    {
        public GameObject duckPrefab;
        public Transform[] spawnPoints;
        public float spawnRadius = 1.0f;
        
        public void SpawnDuck()
        {
            GameObject duck = GameObject.Instantiate(duckPrefab);
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
            duck.transform.position = spawnPoint + Random.insideUnitSphere * spawnRadius;
        }
    }
}