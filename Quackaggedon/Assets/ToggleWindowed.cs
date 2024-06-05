using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWindowed : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Toggle>().isOn = !Screen.fullScreen;
    }
    public void ToggleWindowedMode()
    {
        Screen.fullScreen = !this.GetComponent<Toggle>().isOn;
    }
}
