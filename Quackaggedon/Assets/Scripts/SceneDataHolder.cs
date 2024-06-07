using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class SceneDataHolder : MonoBehaviour
{
    private Dictionary<DuckType, List<GameObject>> allDucksInScene = new Dictionary<DuckType, List<GameObject>>();

    public void StoreDuckData(GameObject duckData)
    {
        if (allDucksInScene.ContainsKey(duckData.GetComponent<DuckData>().duckType))
            allDucksInScene[duckData.GetComponent<DuckData>().duckType].Add(duckData);
        else
            allDucksInScene.Add(duckData.GetComponent<DuckData>().duckType, new List<GameObject>() { duckData });
    }

    public void EquipAllDucksWithUpgrade(DuckType duckType)
    {
        switch (duckType)
        {
            case DuckType.Chef:
                AudioController.Instance.PlayEquipPingSound();
                EquipAllSimpleDucksWithChefBonus();
                break;
            case DuckType.Bread:
                AudioController.Instance.PlayEquipPingSound();
                EquipAllSimpleDucksWithBreadBonus();
                break;
            case DuckType.Magical:
                AudioController.Instance.PlayEquipPingSound();
                EquipAllSimpleDucksWithMagicBonus();
                break;
            case DuckType.Clever:
                AudioController.Instance.PlayEquipPingSound();
                EquipAllSimpleDucksWithCleverBonus();
                break;
            default:
                break;
        }
    }

    private List<GameObject> GetAllDucksInScene(DuckType duckType)
    {
        if (allDucksInScene.ContainsKey(duckType))
            return allDucksInScene[duckType];
        else
            return new List<GameObject>();
    }

    private void EquipAllSimpleDucksWithChefBonus()
    {
        if (allDucksInScene.ContainsKey(DuckType.Simple))
        {
            foreach (var simpleDuck in allDucksInScene[DuckType.Simple])
            {
                if (simpleDuck != null)
                    simpleDuck.transform.GetComponent<SimpleDuckEquipment>().EnableChefBoonVisual();
            }
        }
    }

    private void EquipAllSimpleDucksWithCleverBonus() 
    {
        if (allDucksInScene.ContainsKey(DuckType.Simple))
        {
            foreach (var simpleDuck in allDucksInScene[DuckType.Simple])
            {
                if (simpleDuck != null) 
                    simpleDuck.transform.GetComponent<SimpleDuckEquipment>().EnableCleverBoonVisual();
            }
        }
    }

    private void EquipAllSimpleDucksWithBreadBonus()
    {
        if (allDucksInScene.ContainsKey(DuckType.Simple))
        {
            foreach (var simpleDuck in allDucksInScene[DuckType.Simple])
            {
                if (simpleDuck != null)
                    simpleDuck.transform.GetComponent<SimpleDuckEquipment>().EnableBreadBoonVisual();
            }
        }
    }

    private void EquipAllSimpleDucksWithMagicBonus()
    {
        if (allDucksInScene.ContainsKey(DuckType.Simple))
        {
            foreach (var simpleDuck in allDucksInScene[DuckType.Simple])
            {
                if (simpleDuck != null)
                    simpleDuck.transform.GetComponent<SimpleDuckEquipment>().EnableMagicBoonVisual();
            }
        }
    }
}
