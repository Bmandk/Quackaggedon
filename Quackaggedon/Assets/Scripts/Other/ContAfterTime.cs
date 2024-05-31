using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContAfterTime : MonoBehaviour
{
    public void AutoContinue()
    {
        IntroSceneController.Instance.ContinueToNextScenePart();
    }

    public void HideContinueButton()
    {
        IntroUiHandler.Instance.HideContinueButton();
    }

    public void ShowContinueButton()
    {
        IntroUiHandler.Instance.ShowContinueButton();
    }
}
