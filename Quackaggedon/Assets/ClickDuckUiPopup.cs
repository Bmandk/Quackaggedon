using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickDuckUiPopup : MonoBehaviour
{
    public TextMeshProUGUI quacksReceived;

    public void SetQuacksReceievedOnClick(float amount)
    {
        quacksReceived.text = amount.ToString();
    }
}
