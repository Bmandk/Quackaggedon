using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Animator animator;

    public void ContinueGame()
    {
        LoadPondLevel("DuckClickerScene");
    }

    public void StartGame()
    {
        LoadPondLevel("Intro");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadPondLevel(string level)
    {
        StartCoroutine(LoadLevelAfterBite(level));
    }

    IEnumerator LoadLevelAfterBite(string level)
    {
        animator.SetBool("Eat", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

}
