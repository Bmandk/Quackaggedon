using System.Collections.Generic;
using System.Linq;
using DuckClicker;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public static class SaveManager
{
    private static bool _deleteSave = false;
    private const string _menuName = "Save/Delete Save On Start";
#if UNITY_EDITOR
    static SaveManager()
    {
        EditorApplication.playModeStateChanged += state =>
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (_deleteSave)
                {
                    DeleteSave();
                }
            }
        };
        _deleteSave = EditorPrefs.GetBool(_menuName, false);

        /// Delaying until first editor tick so that the menu
        /// will be populated before setting check state, and
        /// re-apply correct action
        EditorApplication.delayCall += () => { PerformAction(_deleteSave); };
    }
#endif

    public static void Save()
    {
        Dictionary<string, JToken> saveData = new Dictionary<string, JToken>();

        saveData.Add("Currency", CurrencyController.CurrencyAmount);
        SaveMetaSaveData(saveData);

        IEnumerable<ISaveable> dataPersistanceObjects = GetRunDataPersistanceObjects();

        foreach (var dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.Save(saveData);
        }

        string json = JsonConvert.SerializeObject(saveData, Formatting.None);
        var howManyBytes = json.Length * sizeof(char);
        Debug.Log($"Saving data ({howManyBytes}: {json}");
        System.IO.File.WriteAllText(GetSavePath(), json);
    }

    public static void Load()
    {
        if (!System.IO.File.Exists(GetSavePath()))
        {
            return;
        }

        string json = System.IO.File.ReadAllText(GetSavePath());

        Debug.Log("Loading data: " + json);
        JObject jObject = JObject.Parse(json);
        Dictionary<string, JToken> saveData = jObject.ToObject<Dictionary<string, JToken>>();

        if (saveData.TryGetValue("Currency", out JToken currency))
        {
            CurrencyController.SetCurrency(currency.ToObject<double>());
        }

        LoadMetaSaveData(saveData);

        IEnumerable<ISaveable> dataPersistanceObjects = GetRunDataPersistanceObjects();

        foreach (var dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.Load(saveData);
        }
    }

    public static string GetSavePath()
    {
        return Application.persistentDataPath + "/save.json";
    }

    private static IEnumerable<ISaveable> GetRunDataPersistanceObjects()
    {
        IEnumerable<ISaveable> dataPersistanceObjects =
            GameObject.FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveable>();

        return dataPersistanceObjects;
    }

#if UNITY_EDITOR
    [MenuItem("Save/Delete Save")]
#endif
    public static void DeleteSave()
    {
        if (System.IO.File.Exists(GetSavePath()))
        {
            System.IO.File.Delete(GetSavePath());
        }
    }

    public static bool DoesSaveExist()
    {
        return System.IO.File.Exists(GetSavePath());
    }

#if UNITY_EDITOR
    [MenuItem(_menuName)]
    public static void ToggleDeleteSave()
    {
        PerformAction(!_deleteSave);
    }

    private static void PerformAction(bool deleteSave)
    {
        _deleteSave = deleteSave;
        EditorPrefs.SetBool(_menuName, _deleteSave);
        Menu.SetChecked(_menuName, _deleteSave);
    }
#endif

    private static void SaveMetaSaveData(Dictionary<string, JToken> saveData)
    {
        saveData.Add("FoodRevealedCount", DiscoveredObjects.FoodTypesSeen.Count);
        for (int i = 0; i < DiscoveredObjects.FoodTypesSeen.Count; i++)
        {
            saveData.Add($"FoodRevealed{i}", (int)DiscoveredObjects.FoodTypesSeen[i]);
        }

        saveData.Add("DuckRevealedCount", DiscoveredObjects.DuckTypesSeen.Count);
        for (int i = 0; i < DiscoveredObjects.DuckTypesSeen.Count; i++)
        {
            saveData.Add($"DuckRevealed{i}", (int)DiscoveredObjects.DuckTypesSeen[i]);
        }

        var handThrow = PlayerFoodStats.FoodThrownByHand.ToArray();
        saveData.Add("HandThrowCount", PlayerFoodStats.FoodThrownByHand.Count);
        for (int i = 0; i < handThrow.Length; i++)
        {
            saveData.Add($"HandHasThrownEnum{i}", (int) handThrow[i].Key);
            saveData.Add($"HandHasThrown{i}", handThrow[i].Value);
        }

        var duckThrow = PlayerFoodStats.FoodThrownByDuck.ToArray();
        saveData.Add("DuckThrowCount", PlayerFoodStats.FoodThrownByDuck.Count);
        for (int i = 0; i < duckThrow.Length; i++)
        {
            saveData.Add($"DuckHasThrownEnum{i}", (int)duckThrow[i].Key);
            saveData.Add($"DuckHasThrown{i}", duckThrow[i].Value);
        }

        var costOfFoodThrown = PlayerFoodStats.CostOfFoodThrownByHand.ToArray();
        saveData.Add("CostOfFoodThrownCount", PlayerFoodStats.CostOfFoodThrownByHand.Count);
        for (int i = 0; i < costOfFoodThrown.Length; i++)
        {
            saveData.Add($"CostOfFoodThrownEnum{i}", (int)costOfFoodThrown[i].Key);
            saveData.Add($"CostOfFoodThrown{i}", costOfFoodThrown[i].Value);
        }

        var totalFoodThrown = PlayerFoodStats.TotalFoodThrown.ToArray();
        saveData.Add("TotalFoodThrownCount", PlayerFoodStats.TotalFoodThrown.Count);
        for (int i = 0; i < totalFoodThrown.Length; i++)
        {
            saveData.Add($"TotalGoodThrownEnum{i}", (int)totalFoodThrown[i].Key);
            saveData.Add($"TotalFoodThrown{i}", totalFoodThrown[i].Value);
        }
    }

    private static void LoadMetaSaveData(Dictionary<string, JToken> saveData)
    {
        if (saveData.TryGetValue("FoodRevealedCount", out JToken foodRevealedCount))
        {
            for (int i = 0; i < foodRevealedCount.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"FoodRevealed{i}", out JToken foodValue);
                DiscoveredObjects.AddSeenFood((FoodType)foodValue.ToObject<int>());
            }
        }

        if (saveData.TryGetValue("DuckRevealedCount", out JToken duckRevealedCount))
        {
            for (int i = 0; i < (int)duckRevealedCount.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"DuckRevealed{i}", out JToken duckValue);
                DiscoveredObjects.AddSeenDuck((DuckType)duckValue.ToObject<int>());
            }
        }


        if (saveData.TryGetValue("HandThrowCount", out JToken handThrowCount))
        {
            for (int i = 0; i < (int)handThrowCount.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"HandHasThrownEnum{i}", out JToken handValueEnum);
                saveData.TryGetValue($"HandHasThrown{i}", out JToken handValue);

                PlayerFoodStats.AddHandThrownFood((FoodType)handValueEnum.ToObject<int>(), (double)handValue);
            }
        }

        if (saveData.TryGetValue("DuckThrowCount", out JToken duckThrowCount))
        {
            for (int i = 0; i < (int)duckThrowCount.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"DuckHasThrownEnum{i}", out JToken duckValueEnum);
                saveData.TryGetValue($"DuckHasThrown{i}", out JToken duckValue);

                PlayerFoodStats.AddDuckThrownFood((FoodType)duckValueEnum.ToObject<int>(), (double)duckValue);
            }
        }

        if (saveData.TryGetValue("CostOfFoodThrownCount", out JToken costOfFoodCount))
        {
            for (int i = 0; i < (int)costOfFoodCount.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"CostOfFoodThrownEnum{i}", out JToken costFoodEnum);
                saveData.TryGetValue($"CostOfFoodThrown{i}", out JToken costFoodValue);

                PlayerFoodStats.AddTotalCostOfFoodThrownByHand((FoodType)costFoodEnum.ToObject<int>(), (double)costFoodValue);
            }
        }

        if (saveData.TryGetValue("TotalFoodThrownCount", out JToken totalFoodThrownCount))
        {
            for (int i = 0; i < (int)totalFoodThrownCount.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"TotalGoodThrownEnum{i}", out JToken totalFoodThrownEnum);
                saveData.TryGetValue($"TotalFoodThrown{i}", out JToken totalFoodThrownAmount);

                PlayerFoodStats.AddToTotalFoodThrown((FoodType)totalFoodThrownEnum.ToObject<int>(), (double)totalFoodThrownAmount);
            }
        }
    }
}

public interface ISaveable
{
    void Save(Dictionary<string, JToken> saveData);
    void Load(Dictionary<string, JToken> saveData);
}