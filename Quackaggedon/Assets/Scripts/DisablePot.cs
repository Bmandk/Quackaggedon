using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePot : MonoBehaviour
{
    public GameObject potParent;

    public void DisablePotParent()
    {
        potParent.SetActive(false);
    }
}

