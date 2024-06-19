using DuckClicker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    private static References _instance;

    public static References Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        PopulateDuckDataDictionary();
        PopulateFoodDataDictionary();
    }

    [SerializeField]
    private GameObject[] duckPrefabs;

    [SerializeField]
    private GameObject[] foodPrefabs;

    public Collider2D pondCollider;

    public Camera mainCam;

    public Vector3 mouseWorldPos { get; private set; }
    public Vector3 mouseScreenPos { get; private set; }
    
    public DuckStats duckStats;

    public MouseController mouseController;

    public DuckopediaHandler duckopediaHandler; 

    public ArmHandler armHandler;   

    public Transform particleParent;

    public StatsController cookbookController;

    public MenuController menuController;

    public SceneDataHolder sceneDataHolder;

    public ChefPotHandler chefPotHandler;
    
    public DuckopediaDetails duckopediaDetails;

    private void Update()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = -mainCam.transform.position.z; //distance of the plane from the camera.

        mouseWorldPos = mainCam.ScreenToWorldPoint(screenPoint);
        mouseScreenPos = screenPoint;
    }

    private void PopulateFoodDataDictionary()
    {
        foreach (var food in foodPrefabs)
        {
            FoodData foodData = food.GetComponent<FoodData>();
            allFoodData.Add(foodData.foodType, foodData);
        }
    }

    private void PopulateDuckDataDictionary()
    {
        foreach (var duck in duckPrefabs)
        {
            DuckData duckData = duck.GetComponent<DuckData>();
            allDuckData.Add(duckData.duckType, duckData);
        }
    }

    private Dictionary<DuckType, DuckData> allDuckData = new Dictionary<DuckType, DuckData>();
    private Dictionary<FoodType, FoodData> allFoodData = new Dictionary<FoodType, FoodData>();

    public List<DuckData> GetAllDuckDataInOrder()
    {
        var list = new List<DuckData>();  
        foreach (var duck in duckPrefabs)
        {
            list.Add(duck.GetComponent<DuckData>());
        }
        return list;
    }

    public DuckData GetDuckData(DuckType duckType)
    {
        bool succesfullyRetrieved = allDuckData.TryGetValue(duckType, out DuckData duckData);

        if (succesfullyRetrieved)
        {
            return duckData;
        }
        else 
        {
            Debug.LogWarning($"Ducktype {duckType} didn't exist in the list of ducks in the dictionary. Have you forgotten to add its duck-prefab to existing the duckPrefab list?");
            return null; 
        }
    }

    public FoodData GetFoodData(FoodType foodType)
    {
        bool succesfullyRetrieved = allFoodData.TryGetValue(foodType, out FoodData foodData);

        if (succesfullyRetrieved)
        {
            return foodData;
        }
        else
        {
            Debug.LogWarning($"Foodtype {foodType} didn't exist in the list of foods in the dictionary. Have you forgotten to add its food-prefab to existing the foodPrefab list?");
            return null;
        }
    }
}