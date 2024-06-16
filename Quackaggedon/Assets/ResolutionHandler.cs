using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour
{
    public TMP_Dropdown resDropDown;

    Resolution[] allResolutions;
    int selectedResolution;

    List<Resolution> selectedResolutionsList = new List<Resolution>();

    public static int screenHeight;
    public static int screenWidth;

    // Start is called before the first frame update
    void Start()
    {
        allResolutions = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        string newRes;
        foreach (var res in allResolutions)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                selectedResolutionsList.Add(res);
            }
        }
        resDropDown.AddOptions(resolutionStringList);
    }

    public void ChangeResolution()
    {
        selectedResolution = resDropDown.value;
        screenWidth = selectedResolutionsList[selectedResolution].width;
        screenHeight = selectedResolutionsList[selectedResolution].height;
        Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreen);
        SaveManager.SaveScreenSettings();
    }

    public static (int,int) GetSavedWidthHeightOfScreen()
    {
        return (screenWidth, screenHeight);
    }
}
