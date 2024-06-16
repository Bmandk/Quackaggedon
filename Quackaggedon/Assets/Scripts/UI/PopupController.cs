using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField]
    private GameObject _displayDuckUi;
    [SerializeField]
    private Image _displayDuckIcon;
    [SerializeField]
    private TextMeshProUGUI _displayDuckText;

    public void DisplayDuck(DuckData duckData)
    {
        _displayDuckIcon.sprite = duckData.duckDisplayIconRevealed;
        _displayDuckText.text = duckData.duckDisplayName;
        
        _displayDuckUi.SetActive(true);
    }
}
