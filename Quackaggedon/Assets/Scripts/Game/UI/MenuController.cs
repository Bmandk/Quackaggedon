using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject expandSellUI;
    public SceneLoader sceneLoader;
    public GameObject duckopediaUI;
    public GameObject cookbookUI;

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
        CloseDuckopedia();
    }

    public void GoToMainMenu()
    {
        SaveManager.Save();
        sceneLoader.LoadNewScene(SceneLoader.Scene.MainMenu, SceneLoader.Scene.GameScene);
    }

    public void OpenDuckopedia()
    {
        References.Instance.duckopediaHandler.RefreshDuckopedia();
        duckopediaUI.SetActive(true);
    }

    public void CloseDuckopedia()
    {
        duckopediaUI.SetActive(false);
    }
    
    public void ToggleWindowedMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OpenCookbookUI()
    {
        Time.timeScale = 0;
        References.Instance.cookbookController.RefreshCookbook();
        cookbookUI.SetActive(true);
    }

    public void CloseCookBookUi()
    {
        Time.timeScale = 1;
        cookbookUI.SetActive(false);
    }
}
