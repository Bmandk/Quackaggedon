using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContAfterTime : MonoBehaviour
{
    public void AutoContinue()
    {
        IntroSceneController.Instance.ContinueToNextScenePart();
    }
}
