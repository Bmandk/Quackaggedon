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
    public GameObject hutUI;

    public Animator quackStatsAnimator;

    public GameObject[] uiToBeDisabledUponDirt;

    public bool IsBlockingUiOpen()
    {
        return (menuUI.activeSelf || duckopediaUI.activeSelf || cookbookUI.activeSelf || hutUI.activeSelf);
    }

    public void OpenHutUI()
    {
        hutUI.SetActive(true);
    }

    public void CloseHutUI()
    {
        hutUI.SetActive(false);
    }

    public void DisableUiForDirtInteraction()
    {
        foreach (var ui in uiToBeDisabledUponDirt)
        {
            ui.SetActive(false);
        }
    }

    public void EnabbleUiForDirtInteraction()
    {
        AudioController.Instance.PlayBloopSound();
        foreach (var ui in uiToBeDisabledUponDirt)
        {
            ui.SetActive(true);
        }
    }

    public void OpenSellMenu()
    {
        MouseController.selectingDucks = true;
        expandSellUI.SetActive(true);
    }

    public void CloseSellMenu()
    {
        MouseController.selectingDucks = false;
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
        References.Instance.cookbookController.RefreshCookbook();
        cookbookUI.SetActive(true);
    }

    public void CloseCookBookUi()
    {
        cookbookUI.SetActive(false);
    }

    public void PulseQuackStats()
    {
        quackStatsAnimator.SetBool("Retrigger2", true);
        quackStatsAnimator.SetTrigger("Pulse");
    }
}
