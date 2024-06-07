using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookbookController : MonoBehaviour
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
        foreach (var foodTypeDiscovered in DiscoveredObjects.FoodTypesSeen)
        {
            var inst = Instantiate(cookbookEntry, cookBookBtnParent);
            inst.GetComponent<CookbookEntry>().UpdateCookbookEntryValues(foodTypeDiscovered);
        }
    }
}
