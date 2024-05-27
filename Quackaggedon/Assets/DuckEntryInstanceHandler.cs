using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuckEntryInstanceHandler : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image duckIcon;
    public TextMeshProUGUI skillText;

    [SerializeField]
    private Sprite hiddenDuck;
    private string hiddenName = "Undiscovered duck";
    private string hiddenSkill = "Unknown skill";

    public DuckType duckTypeOfCard;

    public void SetEntryToDuck(DuckType duckType)
    {
        duckTypeOfCard = duckType;

        var duckData = References.Instance.GetDuckData(duckType);

        duckIcon.sprite = duckData.duckDisplayIcon;
        nameText.text = duckData.duckDisplayName;
        skillText.text = duckData.duckEffectDescription;
    }

    public void SetEntryToUndiscoveredDuck()
    {
        duckIcon.sprite = hiddenDuck;
        nameText.text = hiddenName;
        skillText.text = hiddenSkill;
    }

    public void SetDuckEnumTypeTo(DuckType duckType)
    {
        duckTypeOfCard = duckType;
    }
}
