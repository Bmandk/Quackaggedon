using UnityEngine;

namespace DuckClicker
{
    public class DuckSpawner : MonoBehaviour
    {
        public float spawnRadius = 1.0f;

        public GameObject SpawnDuck(GameObject duckPrefab, AreaSettings areaSettings)
        {
            GameObject duck = GameObject.Instantiate(duckPrefab);
            Vector3 spawnPoint = Common.Instance.PointInArea();
            duck.transform.position = spawnPoint;

            return duck;
        }
    }
}