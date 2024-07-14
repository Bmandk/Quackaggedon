using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenHandler : MonoBehaviour
{
    public CanvasGroup sceneCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInLoadingScreen(0f, 1f, 0.5f));
    }

    IEnumerator FadeInLoadingScreen(float from, float to, float duration)
    {
        sceneCanvasGroup.alpha = from;
        float elapsedTime = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            sceneCanvasGroup.alpha = Mathf.Lerp(from, to, t/duration);
            yield return null;
        }

        sceneCanvasGroup.alpha = to;

        SceneManager.UnloadSceneAsync(SceneLoader.SceneToUnload);
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneLoader.SceneToLoad, LoadSceneMode.Additive);
        while (!op.isDone)
        {
            yield return null;
        }
        StartCoroutine(FadeOutLoadingScreen(0.5f));

    }

    IEnumerator FadeOutLoadingScreen(float duration)
    {
        sceneCanvasGroup.alpha = 1;
        float elapsedTime = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            sceneCanvasGroup.alpha = Mathf.Lerp(1, 0, t / duration);
            yield return null;
        }

        sceneCanvasGroup.alpha = 0;
        SceneManager.UnloadSceneAsync(SceneLoader.GetSceneName(SceneLoader.Scene.LoadingScreen));
    }
}
