using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject expandSellUI;
    public SceneLoader sceneLoader;
    public GameObject duckopediaUI;
    public GameObject cookbookUI;
    public GameObject hutUI;

    public RectTransform hutButtonFlyPoint;
    public Animator hutButtonSpecilAnim;

    public Transform incEffectParent;

    public Animator quackStatsAnimator;

    public GameObject[] uiToBeDisabledUponDirt;

    public void BigPulseHutButton()
    {
        hutButtonSpecilAnim.SetTrigger("BigPulse");
    }

    public void PlayNotEnoughCoinAnim()
    {
        quackStatsAnimator.SetTrigger("Invalid");
    }

    public void PlayCoinPulseAnim()
    {
        quackStatsAnimator.SetTrigger("Pulse");
    }

    public bool IsBlockingUiOpen()
    {
        return (menuUI.activeSelf || duckopediaUI.activeSelf || cookbookUI.activeSelf || hutUI.activeSelf || RevealHandler.revealIsActive);
    }

    public void OpenHutUI()
    {
        References.Instance.hutRevealController.RevealHutContentsCorrectly();
    }

    public void CloseHutUI()
    {
        References.Instance.hutRevealController.CloseHutUI();
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
    
    public void QuitToDesktop()
    {
        SaveManager.Save();
        Application.Quit();
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
        quackStatsAnimator.SetTrigger("Pulse");
    }
}
