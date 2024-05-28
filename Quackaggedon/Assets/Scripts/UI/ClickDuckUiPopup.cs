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
        quacksReceived.text = amount.ToString();
    }

    public void DestroyUponCompletion()
    {
        Destroy(toDestroyWhenFxsDone.gameObject);
    }
}
