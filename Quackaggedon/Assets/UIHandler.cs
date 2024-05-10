using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject revealUi;
    public Image revealDuckIcon;
    public TextMeshProUGUI revealDuckName;
    public TextMeshProUGUI revealDuckSkillText;

    public void ShowRevealUI(DuckData duckToShow)
    {
        revealDuckName.text = duckToShow.duckDisplayName;
        revealDuckSkillText.text = duckToShow.duckEffectDescription;
        revealDuckIcon.sprite = duckToShow.duckDisplayIcon;
        revealUi.SetActive(true);
    }

    public void CloseRevealUI()
    {
        revealUi.SetActive(false);
    }
}
