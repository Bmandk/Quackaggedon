using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Animator animator;
    public SceneLoader sceneHandler;

    public void ContinueGame()
    {
        LoadPondLevel(SceneLoader.Scene.GameScene);
    }

    public void StartGame()
    {
        LoadPondLevel(SceneLoader.Scene.Intro);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadPondLevel(SceneLoader.Scene level)
    {
        StartCoroutine(LoadLevelAfterBite(level));
    }

    IEnumerator LoadLevelAfterBite(SceneLoader.Scene level)
    {
        animator.SetBool("Eat", true);
        yield return new WaitForSeconds(0.6f);
        sceneHandler.LoadNewScene(level, SceneLoader.Scene.MainMenu);
    }

}
