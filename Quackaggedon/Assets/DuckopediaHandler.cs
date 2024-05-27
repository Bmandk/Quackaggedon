using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckopediaHandler : MonoBehaviour
{
    public Transform duckEntryParent;
    public GameObject duckEntryPrefab;

    private Dictionary<DuckType, DuckEntryInstanceHandler> duckEntries = new Dictionary<DuckType, DuckEntryInstanceHandler>();

    // Start is called before the first frame update
    void Start()
    {
        DeleteEntries();
        PopulateDuckopedia();
    }

    private void DeleteEntries()
    {
        foreach (Transform entry in duckEntryParent)
        {
            Destroy(entry.gameObject);
        }
        duckEntries.Clear();
    }

    private void PopulateDuckopedia()
    {
        var discoveredDucks = DiscoveredObjects.DuckTypesSeen;

        foreach (var discoveredDuck in discoveredDucks)
        {
            var inst = Instantiate(duckEntryPrefab, duckEntryParent);
            DuckEntryInstanceHandler entry = inst.GetComponent<DuckEntryInstanceHandler>();

            entry.SetEntryToDuck(discoveredDuck);
            duckEntries.Add(discoveredDuck, entry);
        }

        var ducks = References.Instance.GetAllDuckData();

        foreach (var duck in ducks)
        {
            if (!duckEntries.ContainsKey(duck.Key))
            {
                var inst = Instantiate(duckEntryPrefab, duckEntryParent);
                DuckEntryInstanceHandler entry = inst.GetComponent<DuckEntryInstanceHandler>();
                duckEntries.Add(duck.Key, entry);
            }
        }
    }

    public void RefreshDuckopedia()
    {
        DeleteEntries();
        PopulateDuckopedia();

        /*
        foreach (var entry in duckEntries)
        {
            if (DiscoveredObjects.HasSeenDuck(entry.Key))
            {
                entry.Value.SetEntryToDuck(entry.Key);
            }
            else
            {
                entry.Value.SetEntryToUndiscoveredDuck();
            }
        }
        */
    }
}
