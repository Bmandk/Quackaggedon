using Febucci.UI;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IntroSceneController : MonoBehaviour
{

    private static IntroSceneController _instance;

    public static IntroSceneController Instance
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
    }

    public GameObject[] scenes;
    private int sceneIndex = 0;

    public GameObject backgroundMusicChill;
    public GameObject backgroundMusicStress;
    public GameObject backgroundMusicEnd;

    public GameObject[] textObjs;

    public GameObject breadText;
    public GameObject beautyText;
    public GameObject simpleText;
    public GameObject megaText;

    public GameObject dreamsText;
    public GameObject perfectText;
    public GameObject pondText;

    public GameObject huhText;
    public GameObject ohnoText;
    public GameObject ducksLiveHereText;
    public GameObject plantedText;
    public GameObject stopText;
    public GameObject sniffleText;
    public GameObject poorDucksText;
    public GameObject wontBreakText;
    public GameObject mansionText;
    public void ContinueToNextScenePart()
    {
        bool skipperTypewriter = false;
        foreach (var t in textObjs)
        {
            if (t.transform.parent.gameObject.activeSelf && !t.GetComponent<TextAnimator_TMP>().allLettersShown)
            {
                skipperTypewriter = true;
                t.GetComponent<TypewriterByCharacter>().SkipTypewriter();
            }
        }

        if (!skipperTypewriter)
            ContinueSceneAsp();
    }

    private void ContinueSceneAsp()
    {
        bool completedText = FinishEventsAndText();
        if (sceneIndex < scenes.Length)
        {
            if (completedText)
            {
                if (sceneIndex != scenes.Length - 1)
                {
                    if (sceneIndex < scenes.Length)
                        scenes[sceneIndex].SetActive(false);
                    sceneIndex++;
                    if (sceneIndex < scenes.Length)
                        scenes[sceneIndex].SetActive(true);

                    if (sceneIndex == 6)
                    {
                        backgroundMusicChill.SetActive(false);
                        backgroundMusicStress.SetActive(true);
                    }
                    else if (sceneIndex == 11)
                    {
                        backgroundMusicChill.SetActive(false);
                        backgroundMusicEnd.SetActive(true);
                    }
                    else if (sceneIndex == 12)
                    {
                        backgroundMusicStress.SetActive(false);
                    }
                }
            }
        }
    }


    public Animator duckTypeAnim;
    public Animator ladyStopsAnim;
    public Animator ladyDreamsAnim;
    public Animator truckAnim;
    public Animator fadeAnim;

    private bool truckPolluted = false;
    private bool putBookAway = false;
    private bool kickedLady = false;
    private bool FinishEventsAndText()
    {
        if (sceneIndex == 1 && !breadText.activeSelf)
        {
            breadText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 1)
        {
            breadText.SetActive(false);
            return true;
        }

        if (sceneIndex == 2 && !beautyText.activeSelf)
        {
            beautyText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 2)
        {
            beautyText.SetActive(false);
            return true;
        }

        if (sceneIndex == 4 && !simpleText.activeSelf && !putBookAway)
        {
            duckTypeAnim.SetBool("LookSimpleDuck", true);
            simpleText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 4 && !megaText.activeSelf && !putBookAway)
        {
            duckTypeAnim.SetBool("LookMegaDuck", true);
            megaText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 4 && !putBookAway)
        {
            putBookAway = true;
            simpleText.SetActive(false);
            megaText.SetActive(false);
            duckTypeAnim.SetBool("CloseBook", true);
            return false;
        }
        else if (sceneIndex == 4)
        {
            return true;
        }

        if (sceneIndex == 5 && !dreamsText.activeSelf)
        {
            ladyDreamsAnim.SetBool("Arm", true);
            dreamsText.SetActive(true);
            return false;
        } /*
        else if (sceneIndex == 5 && !perfectText.activeSelf)
        {
            perfectText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 5 && !pondText.activeSelf)
        {
            pondText.SetActive(true);
            return false;
        } */
        else if (sceneIndex == 5)
        {
            dreamsText.SetActive(false);
            perfectText.SetActive(false);
            pondText.SetActive(false);
            return true;
        }


        if (sceneIndex == 6 && !huhText.activeSelf)
        {
            huhText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 6)
        {
            huhText.SetActive(false);
            return true;
        }

        if (sceneIndex == 7 && !ohnoText.activeSelf)
        {
            ohnoText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 7)
        {
            ohnoText.SetActive(false);
            return true;
        }

        if (sceneIndex == 8 && !ducksLiveHereText.activeSelf)
        {
            ducksLiveHereText.SetActive(true);
            return false;
        }
        //if (sceneIndex == 8 && !plantedText.activeSelf)
        //{
           // plantedText.SetActive(true);
        //    return false;
        //}
        else if (sceneIndex == 8)
        {
            ducksLiveHereText.SetActive(false);
            plantedText.SetActive(false);
            return true;
        }

        if (sceneIndex == 9 && !stopText.activeSelf && !kickedLady)
        {
            stopText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 9 && !kickedLady)
        {
            kickedLady = true;
            ladyStopsAnim.SetBool("KickLady", true);
            stopText.SetActive(false);
            return false;
        }
        else if (sceneIndex == 9)
        {
            return true;
        }

        if (sceneIndex == 10 && !truckPolluted)
        {
            truckPolluted = true;
            truckAnim.SetBool("FinishPolluting", true);
            StartCoroutine(FadeOut(carMusic, 2));
            return false;
        }
        else if (sceneIndex == 10)
        {
            return true;
        }

        if (sceneIndex == 11 && !birdsHaveFlown)
        {
            StartCoroutine(FadeOut(stressMusic, 2));
            birdsHaveFlown = true;
            fadeAnim.SetBool("FadeToBlack", true);
            return false;
        } else if (sceneIndex == 11 && !faded)
        {
            fadeAnim.SetBool("FadeFromBlack", true);
            faded = true;
            return true;
        }

        if (sceneIndex == 12 && !sniffleText.activeSelf)
        {
            sniffleText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 12)
        {
            fadeAnim.SetBool("FadeToBlack", false);
            fadeAnim.SetBool("FadeFromBlack", false);
            sniffleText.SetActive(false);
            return true;
        }

        if (sceneIndex == 13 && !poorDucksText.activeSelf && !hasDoneFinalFade)
        {
            poorDucksText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 13 && !wontBreakText.activeSelf && !hasDoneFinalFade)
        {
            wontBreakText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 13 && !mansionText.activeSelf && !hasDoneFinalFade)
        {
            mansionText.SetActive(true);
            return false;
        }
        else if (sceneIndex == 13 && !hasDoneFinalFade)
        {
            hasDoneFinalFade = true;
            fadeAnim.SetBool("FadeToBlack", true);
            LoadNewGameLvl();
        }
        //else if (sceneIndex == 14)
        //{
             //LoadNewGameLvl();
        //}

        return true;
    }

    public void LoadNewGameLvl()
    {
        sceneHandler.LoadNewScene(SceneLoader.Scene.GameScene, SceneLoader.Scene.Intro);
    }

    public SceneLoader sceneHandler;
    private bool hasDoneFinalFade = false;

    public AudioSource stressMusic;
    public AudioSource carMusic;

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private bool faded;
    private bool birdsHaveFlown;
}
