using UnityEngine;

namespace DuckClicker
{
    public class DuckSpawner : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public float spawnRadius = 1.0f;

        public void SpawnDuck(GameObject duckPrefab)
        {
            GameObject duck = GameObject.Instantiate(duckPrefab);
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            duck.transform.position = spawnPoint + Random.insideUnitSphere * spawnRadius;
        }
    }
}