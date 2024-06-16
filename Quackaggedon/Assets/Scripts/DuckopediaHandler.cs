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

        var ducks = References.Instance.GetAllDuckDataInOrder();

        foreach (DuckData duck in ducks)
        {
            if (!duckEntries.ContainsKey(duck.duckType))
            {
                var inst = Instantiate(duckEntryPrefab, duckEntryParent);
                DuckEntryInstanceHandler entry = inst.GetComponent<DuckEntryInstanceHandler>();
                entry.SetEntryToUndiscoveredDuck(duck.duckType);
                duckEntries.Add(duck.duckType, entry);
            }
        }
    }

    public void RefreshDuckopedia()
    {
        DeleteEntries();
        PopulateDuckopedia();
    }
}
