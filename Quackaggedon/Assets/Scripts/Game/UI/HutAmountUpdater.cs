using System;
using TMPro;
using UnityEngine;

public class HutAmountUpdater : MonoBehaviour
{
    public DuckType duckType;
    private TMP_Text hutAmountText;

    private void Awake()
    {
        hutAmountText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (SaveManager.DidPlayerFinishGame() && duckType == DuckType.Muscle)
            hutAmountText.text = "-";
    }
}