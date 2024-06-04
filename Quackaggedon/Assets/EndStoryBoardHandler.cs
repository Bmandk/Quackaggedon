using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EndStoryBoardHandler : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public AudioSource lastSong;

    public GameObject[] scene3Texts;

    private bool finishedDuckReveal;
    public PlayableDirector newsClip;

    public void ContinueStoryBoard()
    {
        if (!finishedDuckReveal && scene3Texts[1].activeSelf)
        {
            finishedDuckReveal = true;
            scene3Texts[0].SetActive(false);
            scene3Texts[1].SetActive(false);
            newsClip.Play();

        } else
        {
            //scene3Texts[0].SetActive(false);
            scene3Texts[1].SetActive(true);
        }
    }

    public void PlayRampage()
    {

    }

    public void PlayCoolWalk()
    {

    }

    public void LoadMainMenu()
    {
        StartCoroutine(FadeSound(0.5f, lastSong));
        sceneLoader.LoadNewScene(SceneLoader.Scene.MainMenu, SceneLoader.Scene.End);
    }

    IEnumerator FadeSound(float duration, AudioSource audioToSilence)
    {
        float elapsedTime = 0;
        var startValue = audioToSilence.volume;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            audioToSilence.volume = Mathf.Lerp(startValue, 0, t / duration);
            yield return null;
        }

        audioToSilence.volume = 0;
    }

}
