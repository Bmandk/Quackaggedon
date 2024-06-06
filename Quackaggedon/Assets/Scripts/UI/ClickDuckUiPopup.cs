using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickDuckUiPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI quacksReceived;
    [SerializeField]
    private GameObject toDestroyWhenFxsDone;

    public void SetQuacksReceievedOnClick(double amount)
    {
        quacksReceived.text = NumberUtility.FormatNumber(amount);
    }

    public void DestroyUponCompletion()
    {
        Destroy(toDestroyWhenFxsDone.gameObject);
    }
}
