using System.Collections.Generic;
using System.Linq;
using DuckClicker;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public static class SaveManager
{
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
        Debug.Log("Saving data: " + json);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }
    
    public static void Load()
    {
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            return;
        }
        
        string json = PlayerPrefs.GetString("SaveData");
        Debug.Log("Loading data: " + json);
        JObject jObject = JObject.Parse(json);
        Dictionary<string, JToken> saveData = jObject.ToObject<Dictionary<string, JToken>>();
        
        if (saveData.TryGetValue("Currency", out JToken currency))
        {
            CurrencyController.SetCurrency((float) currency.ToObject<float>());
        }

        LoadMetaSaveData(saveData);
        
        IEnumerable<ISaveable> dataPersistanceObjects = GetRunDataPersistanceObjects();
        
        foreach (var dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.Load(saveData);
        }
    }
    
    private static IEnumerable<ISaveable> GetRunDataPersistanceObjects()
    {
        IEnumerable<ISaveable> dataPersistanceObjects =
            GameObject.FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveable>();
        
        return dataPersistanceObjects;
    }
    
    [MenuItem("Save/Delete Save")]
    public static void DeleteSave()
    {
        PlayerPrefs.DeleteKey("SaveData");
        PlayerPrefs.Save();
    }

    private static void SaveMetaSaveData(Dictionary<string, JToken> saveData)
    {
        saveData.Add("FoodRevealedCount", DiscoveredObjects.FoodTypesSeen.Count);
        for (int i = 0; i < DiscoveredObjects.FoodTypesSeen.Count; i++)
        {
            saveData.Add($"FoodRevealed{i}", (int) DiscoveredObjects.FoodTypesSeen[i]);
        }
        saveData.Add("DuckRevealedCount", DiscoveredObjects.DuckTypesSeen.Count);
        for (int i = 0; i < DiscoveredObjects.DuckTypesSeen.Count; i++)
        {
            saveData.Add($"DuckRevealed{i}", (int)DiscoveredObjects.DuckTypesSeen[i]);
        }
    }

    private static void LoadMetaSaveData(Dictionary<string, JToken> saveData)
    {
        if (saveData.TryGetValue("FoodRevealedCount", out JToken foodRevealedCount))
        {
            for (int i = 0; i < foodRevealedCount.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"FoodRevealed{i}", out JToken foodValue);
                DiscoveredObjects.AddSeenFood((FoodType) foodValue.ToObject<int>());
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
    }
}

public interface ISaveable
{
    void Save(Dictionary<string, JToken> saveData);
    void Load(Dictionary<string, JToken> saveData);
}