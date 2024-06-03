using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUiHandler : MonoBehaviour
{
    private static IntroUiHandler _instance;

    public static IntroUiHandler Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject continueButton;

    public void HideContinueButton()
    {
        continueButton.SetActive(false);
    }

    public void ShowContinueButton()
    {
        continueButton.SetActive(true);
    }
}
