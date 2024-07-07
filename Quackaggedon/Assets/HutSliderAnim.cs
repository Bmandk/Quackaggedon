using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutSliderAnim : MonoBehaviour
{
    public Animator sliderAnim;

    public void PulseSlider()
    {
        if (sliderAnim.isActiveAndEnabled)
        {
            AudioController.Instance.PlayBloopSound();
            sliderAnim.SetTrigger("SliderPulse");
        }
    }
}
