using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDuckEquipment : MonoBehaviour
{
    [SerializeField]
    private GameObject miniCrown, miniBow, miniAbs, miniChef;

    public void EnableBreadBoonVisual()
    {
        miniAbs.SetActive(true);
    }

    public void EnableCleverBoonVisual()
    {
        miniBow.SetActive(true);
    }

    public void EnableMagicBoonVisual()
    {
        miniCrown.SetActive(true);
    }

    public void EnableChefBoonVisual()
    {
        miniChef.SetActive(true);
    }

    public void EnableCorrectVisualOnSpawn()
    {
        if (DiscoveredObjects.HasSeenDuck(DuckType.Chef))
            EnableChefBoonVisual();

        if (DiscoveredObjects.HasSeenDuck(DuckType.Bread))
            EnableBreadBoonVisual();

        if (DiscoveredObjects.HasSeenDuck(DuckType.Magical))
            EnableMagicBoonVisual();

        if (DiscoveredObjects.HasSeenDuck(DuckType.Clever))
            EnableCleverBoonVisual();
    }


}
