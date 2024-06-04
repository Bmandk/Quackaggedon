using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
        WinScreen,
        End,
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
        { Scene.WinScreen, "WinScreen" },
        { Scene.End, "End" },
    };

    public void LoadNewScene(Scene sceneToLoad, Scene sceneToUnload, AudioMixerGroup audioToSilence = null)
    {
        if (audioToSilence != null)
        {
            StartCoroutine(FadeSound(0.5f, audioToSilence));
        }

        SceneToLoad = sceneNames[sceneToLoad];
        SceneToUnload = sceneNames[sceneToUnload];
        SceneManager.LoadScene(sceneNames[Scene.LoadingScreen], LoadSceneMode.Additive);
    }

    IEnumerator FadeSound(float duration, AudioMixerGroup audioToSilence)
    {
        float elapsedTime = 0;
        float startValue;
        audioToSilence.audioMixer.GetFloat("volume", out startValue);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            audioToSilence.audioMixer.SetFloat("volume", Mathf.Lerp(startValue, -80, t / duration));
            yield return null;
        }

        audioToSilence.audioMixer.SetFloat("volume", -80);
    }
}
