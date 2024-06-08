using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHutVisual : MonoBehaviour
{
    public GameObject toDisableOnUnlock;
    public GameObject toEnableOnUnlock;

    public void Unlock()
    {
        toDisableOnUnlock.SetActive(false);
        toEnableOnUnlock.SetActive(true);
    }
}
