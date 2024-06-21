using System.Collections.Generic;
using UnityEngine;

namespace DuckClicker
{
    public class DuckSpawner : MonoBehaviour
    {
        public float spawnRadius = 1.0f;
        public int maxDucksInPond = 100;
        public int minDucksPerType = 1;

        public GameObject SpawnDuck(GameObject duckPrefab, AreaSettings areaSettings, bool loadingFromSave)
        {
            GameObject duck = GameObject.Instantiate(duckPrefab);

            if (loadingFromSave)
            {
                duck.GetComponent<DuckMovementHandler>().duckAnim.SetBool("LoadFromSave", true);
            } 
            else
            {
                duck.GetComponent<DuckMovementHandler>().duckAnim.SetBool("SpawnAtRuntime", true);
            }

            Vector3 spawnPoint = Common.Instance.PointInArea();
            duck.transform.position = spawnPoint;

            CheckRatios();

            return duck;
        }

        private static readonly List<DuckType> duckTypeOrder = new List<DuckType>
        {
            DuckType.Simple,
            DuckType.Clever,
            DuckType.Bread,
            DuckType.Chef,
            DuckType.Magical
        };

        private void CheckRatios()
        {
            if (DuckAmounts.GetTotalDucksInPond() <= maxDucksInPond)
                return;
            
            Dictionary<DuckType, float> duckRatios = new Dictionary<DuckType, float>();
            foreach (var duckType in DuckData.duckObjects.Keys)
            {
                if (DuckData.duckObjects[duckType].Count == 0)
                    continue;
                duckRatios.Add(duckType, (float)DuckAmounts.hutAmounts[duckType] / DuckData.duckObjects[duckType].Count);
            }
            
            List<DuckType> sortedDuckTypes = new List<DuckType>(duckRatios.Keys);
            // Sort by duck ratio, then by order in duckTypeOrder
            sortedDuckTypes.Sort((duckType1, duckType2) =>
            {
                int ratioComparison = duckRatios[duckType1].CompareTo(duckRatios[duckType2]);
                if (ratioComparison != 0)
                    return ratioComparison;
                
                return duckTypeOrder.IndexOf(duckType1).CompareTo(duckTypeOrder.IndexOf(duckType2));
            });

            foreach (var duckType in sortedDuckTypes)
            {
                if (DuckAmounts.GetTotalDucksInPond(duckType) > minDucksPerType)
                {
                    MoveDuckToHut(duckType);
                    return; // We can safely return since we will check for every spawned duck, so the total ducks will always be less than maxDucksInPond after this
                }
            }
        }

        private void MoveDuckToHut(DuckType duckType)
        {
            Debug.Log($"Moving duck of type {duckType} to hut");
            DuckData duckData = DuckData.duckObjects[duckType][0];
            Destroy(duckData.gameObject);
            DuckData.duckObjects[duckType].Remove(duckData);
            
            DuckAmounts.hutAmounts[duckType]++;
        }
    }
}