using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinContinueButton : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneLoader sceneHandler = FindObjectOfType<SceneLoader>();
        sceneHandler.LoadNewScene(SceneLoader.Scene.MainMenu, SceneLoader.Scene.WinScreen);
    }
}
