using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EndStoryBoardHandler : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public AudioSource lastSong;

    public GameObject[] scene3Texts;
    public GameObject[] scene4Texts;
    public GameObject[] scene5Texts;
    public GameObject[] scene6Texts;

    private bool finishedDuckReveal;
    private bool finishedApprovingLady;
    private bool startedNewsAnchor;
    private bool startedRampage;
    private bool thrownBuilding;
    private bool startedLastWalk;


    public PlayableDirector ladyApproves;
    public PlayableDirector newsClip;
    public PlayableDirector rampageClip;
    public PlayableDirector coolWalkClip;

    public GameObject scene3;
    public GameObject scene4;
    public GameObject scene5;
    public GameObject scene6;
    public GameObject scene5Text;
    public GameObject scene5Ui;
    public GameObject scene6Ui;
    public GameObject scene6Text;

    public Animator scene4Anim;
    public Animator scene6Anim;

    public GameObject continueBtn;

    public FadePausContinuer fadePausContinuer;

    private bool IsAnyTextPlaying()
    {
        var allText = scene3Texts.Concat(scene4Texts).Concat(scene5Texts).Concat(scene6Texts);
        //bool skipperTypewriter = false;
        foreach (var text in allText) 
        {
            if (text.transform.gameObject.activeSelf && !text.transform.GetChild(1).GetComponent<TextAnimator_TMP>().allLettersShown)
            {                
                //skipperTypewriter = true;
                text.transform.GetChild(1).GetComponent<TypewriterByCharacter>().SkipTypewriter();
                return true;
            }
        }

        return false;
    }

    public void ContinueStoryBoard()
    {
        if (!IsAnyTextPlaying())
        {
            ContinueStoryFlow();
            //continueBtn.SetActive(false);
        }
    }

    private void ContinueStoryFlow()
    {
        if (!finishedDuckReveal && scene3Texts[1].activeSelf)
        {
            finishedDuckReveal = true;
            scene3Texts[0].SetActive(false);
            scene3Texts[1].SetActive(false);
            scene3.SetActive(false);
            continueBtn.SetActive(false);
            ladyApproves.Play();

        }
        else if (!finishedDuckReveal)
        {
            scene3Texts[1].SetActive(true);
        }
        else if (finishedDuckReveal && !finishedApprovingLady)
        {
            scene4Texts[1].SetActive(true);
            scene4Anim.SetTrigger("Angry");
            finishedApprovingLady = true;
        }
        else if (finishedApprovingLady && !startedNewsAnchor)
        {
            scene4Texts[0].SetActive(false);
            scene4Texts[1].SetActive(false);
            continueBtn.SetActive(false);
            fadePausContinuer.gameObject.SetActive(true);
            startedNewsAnchor = true;
        }
        else if (startedNewsAnchor && !scene5Texts[0].activeSelf)
        {
            scene4.SetActive(false);
            newsClip.Play();
        }
        else if (startedNewsAnchor && !scene5Texts[1].activeSelf)
        {
            scene5Texts[1].SetActive(true);
        }
        else if (startedNewsAnchor && !scene5Texts[2].activeSelf)
        {
            scene5Texts[2].SetActive(true);
        }
        else if (startedNewsAnchor && !startedRampage)
        {
            startedRampage = true;
            scene5.SetActive(false);
            scene5Ui.SetActive(false);
            scene5Text.SetActive(false);
            continueBtn.SetActive(false);
            rampageClip.Play();
        }
        else if (startedRampage && !scene6Texts[1].activeSelf)
        {
            scene6Texts[0].SetActive(false);
            scene6Anim.SetTrigger("Look");
            scene6Texts[1].SetActive(true);
        }
        else if (startedRampage && !scene6Texts[2].activeSelf)
        {
            scene6Texts[2].SetActive(true);
        }
        else if (startedRampage && !scene6Texts[3].activeSelf)
        {
            scene6Texts[3].SetActive(true);
        }
        else if (startedRampage && !scene6Texts[4].activeSelf)
        {
            scene6Texts[4].SetActive(true);
        }
        else if (startedRampage && !thrownBuilding)
        {
            thrownBuilding = true;
            continueBtn.SetActive(false);
            scene6Text.SetActive(false);
            scene6Anim.SetTrigger("ThrowBuilding");
        }
        else if (!startedLastWalk)
        {

            scene6Ui.SetActive(false);
            scene6.SetActive(false);
            startedLastWalk = true;
            coolWalkClip.Play();
        }
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
