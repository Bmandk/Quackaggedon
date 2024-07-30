using System.Collections.Generic;
using System.IO;
using System.Linq;
using DuckClicker;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Steamworks;
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

        saveData.Add("PlayerFinishedGame", EndStarter.hasPlayerFinishedGame);
        
        saveData.Add("tutorialIndex", TutorialController.TutorialIndex);

        saveData.Add("revealedLvl1Hut", HutRevealController.revealedLvl1);
        saveData.Add("revealedLvl2Hut", HutRevealController.revealedLvl2);
        saveData.Add("revealedLvl3Hut", HutRevealController.revealedLvl3);
        saveData.Add("duckIconsThatShouldBeRevealed", HutRevealController.duckIconsThatShouldBeRevealed);
        saveData.Add("amountOfDuckIconsAlreadyRevealed", HutRevealController.amountOfDuckIconsAlreadyRevealed);

        SaveMetaSaveData(saveData);

        IEnumerable<ISaveable> dataPersistanceObjects = GetRunDataPersistanceObjects();

        foreach (var dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.Save(saveData);
        }

        string json = JsonConvert.SerializeObject(saveData, Formatting.None);
        var howManyBytes = json.Length * sizeof(char);
        Debug.Log($"Saving data ({howManyBytes}: {json}");
        string savePath = GetSavePath();
        CreateFolderIfNecessary(System.IO.Path.GetDirectoryName(savePath));
        System.IO.File.WriteAllText(savePath, json);
    }
    
    private static bool GetSteamID(out string steamID)
    {
        if (SteamManager.Initialized == false)
        {
            steamID = "0";
            return false;
        }
        var id = SteamUser.GetSteamID();
        steamID = id.ToString();
        return id.IsValid();
    }

    public static bool Load()
    {
        if (!System.IO.File.Exists(GetSavePath()))
        {
            return false;
        }

        string json = System.IO.File.ReadAllText(GetSavePath());

        Debug.Log("Loading data: " + json);
        JObject jObject = JObject.Parse(json);
        Dictionary<string, JToken> saveData = jObject.ToObject<Dictionary<string, JToken>>();

        if (saveData.TryGetValue("Currency", out JToken currency))
        {
            CurrencyController.SetCurrency(currency.ToObject<double>());
        }

        if (saveData.TryGetValue("PlayerFinishedGame", out JToken playerFinishedGame))
        {
            EndStarter.hasPlayerFinishedGame = playerFinishedGame.ToObject<bool>();
        }
        
        if (saveData.TryGetValue("tutorialIndex", out JToken tutorialIndex))
        {
            TutorialController.TutorialIndex = tutorialIndex.ToObject<int>();
        }

        if (saveData.TryGetValue("revealedLvl1Hut", out JToken revealedLvl1Hut))
        {
            HutRevealController.revealedLvl1 = revealedLvl1Hut.ToObject<bool>();
        }

        if (saveData.TryGetValue("revealedLvl2Hut", out JToken revealedLvl2Hut))
        {
            HutRevealController.revealedLvl2 = revealedLvl2Hut.ToObject<bool>();
        }

        if (saveData.TryGetValue("revealedLv13Hut", out JToken revealedLvl3Hut))
        {
            HutRevealController.revealedLvl3 = revealedLvl3Hut.ToObject<bool>();
        }

        if (saveData.TryGetValue("duckIconsThatShouldBeRevealed", out JToken duckIconsThatShouldBeRevealed))
        {
            HutRevealController.duckIconsThatShouldBeRevealed = duckIconsThatShouldBeRevealed.ToObject<int>();
        }

        if (saveData.TryGetValue("amountOfDuckIconsAlreadyRevealed", out JToken amountOfDuckIconsAlreadyRevealed))
        {
            HutRevealController.amountOfDuckIconsAlreadyRevealed = amountOfDuckIconsAlreadyRevealed.ToObject<int>();
        }

        LoadMetaSaveData(saveData);

        IEnumerable<ISaveable> dataPersistanceObjects = GetRunDataPersistanceObjects();

        foreach (var dataPersistanceObject in dataPersistanceObjects)
        {
            dataPersistanceObject.Load(saveData);
        }
        return true;
    }

    public static string GetSavePath()
    {
        bool hasSteamID = GetSteamID(out string steamID);
        
        if (hasSteamID)
        {
            return Path.Combine(Application.persistentDataPath, steamID, "save.json");
        }
        return Path.Combine(Application.persistentDataPath, "0", "save.json");
    }
    
    public static string CreateFolderIfNecessary(string path)
    {
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        return path;
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
        EndStarter.hasPlayerFinishedGame = false;
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
            saveData.Add($"HandHasThrownEnum{i}", (int)handThrow[i].Key);
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

        var totalTimesDuckClicked = PlayerFoodStats.TotalTimesDuckClicked.ToArray();
        saveData.Add("TotalTimesDuckClickedCount", PlayerFoodStats.TotalTimesDuckClicked.Count);
        for (int i = 0; i < totalTimesDuckClicked.Length; i++)
        {
            saveData.Add($"TotalTimesDuckClickedEnum{i}", (int)totalTimesDuckClicked[i].Key);
            saveData.Add($"TotalTimesDuckClicked{i}", totalTimesDuckClicked[i].Value);
        }

        saveData.Add("FoodTypesAfforded", DiscoveredObjects.FoodTypesAfforded.Count);
        for (int i = 0; i < DiscoveredObjects.FoodTypesAfforded.Count; i++)
        {
            saveData.Add($"FoodTypesAfforded{i}", (int)DiscoveredObjects.FoodTypesAfforded[i]);
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

        if (saveData.TryGetValue("TotalTimesDuckClickedCount", out JToken totalTimesDuckClicked))
        {
            for (int i = 0; i < (int)totalTimesDuckClicked.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"TotalTimesDuckClickedEnum{i}", out JToken totalTimesDuckClickedEnum);
                saveData.TryGetValue($"TotalTimesDuckClicked{i}", out JToken totalTimesDuckClickedAmount);

                PlayerFoodStats.AddTimesDuckClicked((DuckType)totalTimesDuckClickedEnum.ToObject<int>(), (double)totalTimesDuckClickedAmount);
            }
        }

        if (saveData.TryGetValue("FoodTypesAfforded", out JToken foodTypesAffordedCount))
        {
            for (int i = 0; i < foodTypesAffordedCount.ToObject<int>(); i++)
            {
                saveData.TryGetValue($"FoodTypesAfforded{i}", out JToken foodTypesAffordedValue);
                DiscoveredObjects.AddAffordedFood((FoodType)foodTypesAffordedValue.ToObject<int>());
            }
        }
    }


    public static void SaveScreenSettings()
    {
        PlayerPrefs.SetInt("Windowed", (ToggleWindowed.isWindow ? 1 : 0));
        PlayerPrefs.SetInt("ScreenHeight", ResolutionHandler.screenHeight);
        PlayerPrefs.SetInt("ScreenWidth", ResolutionHandler.screenWidth);


        bool isWindowed = (PlayerPrefs.GetInt("Windowed") != 0);
    }

    public static (int, int) GetScreenWidthHeight()
    {
        if (PlayerPrefs.HasKey("ScreenHeight"))
        {
            return (PlayerPrefs.GetInt("ScreenWidth"), PlayerPrefs.GetInt("ScreenHeight"));
        }
        else
        {
            return (-1, -1);
        }
    }

    private static void AddDataPoint(string key, JToken value, Dictionary<string, JToken> alreadySavedData)
    {
        if (alreadySavedData.ContainsKey(key))
        {
            alreadySavedData[key] = value;
        }
        else
        {
            alreadySavedData.Add(key, value);
        }
    }

    public static bool DidPlayerFinishGame()
    {
        if (!System.IO.File.Exists(GetSavePath()))
        {
            return false;
        }

        string json = System.IO.File.ReadAllText(GetSavePath());

        Debug.Log("Loading data: " + json);
        JObject jObject = JObject.Parse(json);
        Dictionary<string, JToken> saveData = jObject.ToObject<Dictionary<string, JToken>>();

        if (saveData.TryGetValue("PlayerFinishedGame", out JToken finishedGame))
        {
            return finishedGame.ToObject<bool>();
        }
        return false;
    }
}


public interface ISaveable
{
    void Save(Dictionary<string, JToken> saveData);
    void Load(Dictionary<string, JToken> saveData);
}