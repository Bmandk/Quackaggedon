using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    [SerializeField]
    private GameObject cookbookUI;

    [SerializeField]
    private Transform cookBookBtnParent;

    [SerializeField]
    private GameObject cookbookEntry;

    public void RefreshCookbook()
    {
        // Destroy old outdated buttons
        foreach (Transform btn in cookBookBtnParent)
        {
            Destroy(btn.gameObject);
        }

        // Add new updated buttons
        foreach (var duckTypeDiscovered in DiscoveredObjects.DuckTypesSeen)
        {
            FoodType food = DuckUnlockData.GetWhichFoodsNeededToUnlockDuck(duckTypeDiscovered);
            var inst = Instantiate(cookbookEntry, cookBookBtnParent);
            inst.GetComponent<StatsEntry>().SetStatusType(food, true);
        }

        if (SaveManager.DidPlayerFinishGame())
        {
            FoodType food = DuckUnlockData.GetWhichFoodsNeededToUnlockDuck(DuckType.Muscle);
            var inst = Instantiate(cookbookEntry, cookBookBtnParent);
            inst.GetComponent<StatsEntry>().SetStatusType(food, true);
        }

        var ducks = References.Instance.GetAllDuckDataInOrder();

        if (!SaveManager.DidPlayerFinishGame())
        {
            foreach (DuckData duck in ducks)
            {
                if (!DiscoveredObjects.HasSeenDuck(duck.duckType))
                {
                    FoodType food = DuckUnlockData.GetWhichFoodsNeededToUnlockDuck(duck.duckType);
                    var inst = Instantiate(cookbookEntry, cookBookBtnParent);
                    DuckEntryInstanceHandler entry = inst.GetComponent<DuckEntryInstanceHandler>();
                    inst.GetComponent<StatsEntry>().SetStatusType(food, false);
                }
            }
        }
    }
}
