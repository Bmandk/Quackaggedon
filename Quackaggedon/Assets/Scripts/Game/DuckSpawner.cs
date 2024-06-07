using UnityEngine;

namespace DuckClicker
{
    public class DuckSpawner : MonoBehaviour
    {
        public float spawnRadius = 1.0f;

        public void SpawnDuck(GameObject duckPrefab, AreaSettings areaSettings)
        {
            GameObject duck = GameObject.Instantiate(duckPrefab);
            //Transform[] spawnPoints = areaSettings.SpawnPoints;
            //Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            Vector3 spawnPoint = Common.Instance.PointInArea();
            duck.transform.position = spawnPoint;
        }
    }
}