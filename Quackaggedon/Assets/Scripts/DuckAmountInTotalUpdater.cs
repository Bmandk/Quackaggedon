using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuckAmountInTotalUpdater : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AreaSettings.CurrentArea != null)
        {
            text.text = $"{DuckAmounts.GetTotalDucks()}";
        }
    }
}
