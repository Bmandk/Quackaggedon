using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DuckClicker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuckopediaDetails : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image duckIcon;
    public TextMeshProUGUI skillText;
    public TextMeshProUGUI duckAmount;
    
    public void SetEntryToDuck(DuckType duckType)
    {

        var duckData = References.Instance.GetDuckData(duckType);

        duckIcon.sprite = duckData.duckDisplayIcon;
        nameText.text = duckData.duckDisplayName;

        // Duck descriptions have arguments supplied by this list
        List<string> duckBonuses = new List<string>();

        double totalQPS = CurrencyController.QuacksPerSecond;
        long simpleDuckAmount = DuckAmounts.duckCounts[DuckType.Simple][1];
        
        string totalQPSFormatted = NumberUtility.FormatNumber(totalQPS);
        string qpsPerSimpleDuck = NumberUtility.FormatNumber(totalQPS / simpleDuckAmount);
        string chefDuckTimer = DuckFeeder.ChefDuckTimer.ToString("F1", CultureInfo.InvariantCulture);
        string foodThrown = NumberUtility.FormatNumber(DuckFeeder.CleverDuckAmount);
        
        switch (duckType)
        {
            case DuckType.Simple:
                duckBonuses.Add(qpsPerSimpleDuck);
                duckBonuses.Add(totalQPSFormatted);
                break;
            case DuckType.Chef:
                duckBonuses.Add(chefDuckTimer);
                duckBonuses.Add(foodThrown);
                break;
            case DuckType.Bread:
                duckBonuses.Add(qpsPerSimpleDuck);
                break;
            case DuckType.Magical:
                duckBonuses.Add(qpsPerSimpleDuck);
                duckBonuses.Add(foodThrown);
                duckBonuses.Add(chefDuckTimer);
                break;
            case DuckType.Clever:
                duckBonuses.Add(foodThrown);
                break;
        }
        
        skillText.text = string.Format(duckData.duckDetailsDescription, duckBonuses.ToArray());

        duckAmount.text = ColorLong(DuckAmounts.duckCounts[duckType][1]).ToString();
        
        transform.parent.gameObject.SetActive(true);
    }
    
    private string ColorLong(long number)
    {
        if (number <= 1)
        {
            return number.ToString();
        }
        else
        {
            return $"<color=green>{number}</color>";
        }
    }

    public void Close()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
