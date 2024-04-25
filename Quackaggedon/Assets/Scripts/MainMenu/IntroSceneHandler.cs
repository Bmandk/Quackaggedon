using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneHandler : MonoBehaviour
{
    public void LoadPondLevel()
    {
        SceneManager.LoadScene("DuckClickerScene", LoadSceneMode.Single);
    }
}
