using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //Variables are used by the loading screen to load and unload scenes correctly
    public static string SceneToLoad { get; private set; }
    public static string SceneToUnload { get; private set; }

    public enum Scene
    {
        MainMenu,
        LoadingScreen,
        Intro,
        GameScene,
    }

    public static string GetSceneName(Scene scene)
    {
        return sceneNames[scene];
    }

    private static  Dictionary<Scene, string> sceneNames = new Dictionary<Scene, string>()
    {
        { Scene.MainMenu, "MainMenu" },
        { Scene.LoadingScreen, "LoadingScreen" },
        { Scene.Intro, "Intro" },
        { Scene.GameScene, "DuckClickerScene" },
    };

    public void LoadNewScene(Scene sceneToLoad, Scene sceneToUnload)
    {
        SceneToLoad = sceneNames[sceneToLoad];
        SceneToUnload = sceneNames[sceneToUnload];
        SceneManager.LoadScene(sceneNames[Scene.LoadingScreen], LoadSceneMode.Additive);
    }
}
