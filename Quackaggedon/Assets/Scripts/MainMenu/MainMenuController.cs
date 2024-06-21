using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private static MainMenuController _instance;

    public static MainMenuController Instance
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
        //if (PlayerPrefs.HasKey("Volume"))
        //{
        //    AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        //}
    }

    public Animator animator;
    public SceneLoader sceneHandler;
    public AudioMixerGroup mixerGroup;
    public GameObject warningMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    public GameObject gigiaDuckInCredits;

    public void OpenWarningMenu()
    {
        if (SaveManager.DoesSaveExist())
            warningMenu.SetActive(true);
        else
            StartGame();
    }

    public void CloseWarningMenu()
    {
        warningMenu?.SetActive(false);
    }

    public void ContinueGame()
    {
        LoadPondLevel(SceneLoader.Scene.GameScene, mixerGroup);
    }

    public void StartGame()
    {
        SaveManager.DeleteSave();
        LoadPondLevel(SceneLoader.Scene.Intro, mixerGroup);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadPondLevel(SceneLoader.Scene level, AudioMixerGroup soundToSilence)
    {
        StartCoroutine(LoadLevelAfterBite(level, soundToSilence));
    }

    IEnumerator LoadLevelAfterBite(SceneLoader.Scene level, AudioMixerGroup soundToSilence)
    {
        animator.SetBool("Eat", true);
        yield return new WaitForSeconds(0.6f);
        sceneHandler.LoadNewScene(level, SceneLoader.Scene.MainMenu, soundToSilence);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        if (SaveManager.DidPlayerFinishGame())
            gigiaDuckInCredits.SetActive(true);
        else 
            gigiaDuckInCredits.SetActive(false);

        creditsMenu.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsMenu.SetActive(false);
    }
}
