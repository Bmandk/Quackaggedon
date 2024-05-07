using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuUI;
    public SceneLoader sceneLoader;

    public void OpenMenu()
    {
        menuUI.SetActive(true);
    }

    public void CloseMenu()
    {
        menuUI.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SaveManager.Save();
        sceneLoader.LoadNewScene(SceneLoader.Scene.MainMenu, SceneLoader.Scene.GameScene);
    }
}
