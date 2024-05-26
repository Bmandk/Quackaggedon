using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject expandSellUI;
    public SceneLoader sceneLoader;

    public void OpenSellMenu()
    {
        MouseController.selectingDucks = true;
        Time.timeScale = 0;
        expandSellUI.SetActive(true);
    }

    public void CloseSellMenu()
    {
        MouseController.selectingDucks = false;
        Time.timeScale = 1;
        expandSellUI.SetActive(false);
    }

    public void SellDuck()
    {
        var selectedDucks = References.Instance.mouseController.GetAllSelectedDucks();

        foreach (var duck in selectedDucks)
        {
            Destroy(duck.gameObject);
        }

        References.Instance.mouseController.ResetAllSelectedDucks();
    }

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
