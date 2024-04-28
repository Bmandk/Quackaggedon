using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Animator animator;
    public SceneHandler sceneHandler;

    public void ContinueGame()
    {
        LoadPondLevel(SceneHandler.Scene.GameScene);
    }

    public void StartGame()
    {
        LoadPondLevel(SceneHandler.Scene.Intro);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadPondLevel(SceneHandler.Scene level)
    {
        StartCoroutine(LoadLevelAfterBite(level));
    }

    IEnumerator LoadLevelAfterBite(SceneHandler.Scene level)
    {
        animator.SetBool("Eat", true);
        yield return new WaitForSeconds(0.6f);
        sceneHandler.LoadNewScene(level, SceneHandler.Scene.MainMenu);
    }

}
