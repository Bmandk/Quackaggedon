using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuckAmountInHutUpdater : MonoBehaviour
{
    private TMP_Text text;
    public Slider slider;

    private long prevValue;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AreaSettings.CurrentArea != null)
        {
            //text.text = $"{DuckAmounts.GetTotalDucksInHut()}/{HutRevealController.maxLevelDuckAmount}";
            var duckHutAmount = DuckAmounts.GetTotalDucksInHut();

            if (prevValue != duckHutAmount) 
            {
                References.Instance.hutSliderAnim.PulseSlider();
            }

            prevValue = duckHutAmount;

            text.text = $"{duckHutAmount}";
            slider.value = Mathf.Min(1, DuckAmounts.GetTotalDucksInHut()/HutRevealController.maxLevelDuckAmount);
        }

        References.Instance.hutRevealController.UpdateUIIfNewDucksInHut();
    }
}
