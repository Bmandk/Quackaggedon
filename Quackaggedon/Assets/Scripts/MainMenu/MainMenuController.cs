using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator animator;
    public SceneLoader sceneHandler;
    public AudioMixerGroup mixerGroup;
    public GameObject warningMenu;

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

}
