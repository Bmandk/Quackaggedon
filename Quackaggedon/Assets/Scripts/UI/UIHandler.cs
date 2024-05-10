using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private static UIHandler _instance;

    public static UIHandler Instance
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

    [SerializeField]
    private GameObject revealUi;
    [SerializeField]
    private Image revealDuckIcon;
    [SerializeField]
    private TextMeshProUGUI revealDuckName, revealDuckSkillText;
    [SerializeField]
    private CanvasGroup revealCanvasGroup;

    private Coroutine revealC;
    public void ShowRevealUI(DuckData duckToShow)
    {
        revealDuckName.text = duckToShow.duckDisplayName;
        revealDuckSkillText.text = duckToShow.duckEffectDescription;
        revealDuckIcon.sprite = duckToShow.duckDisplayIcon;
        revealCanvasGroup.alpha = 0;
       
        revealUi.SetActive(true);

        if (revealC != null)
            StopCoroutine(revealC);
        revealC = StartCoroutine(FadeInCanvasGroup(0, 1, 0.5f, revealCanvasGroup));
    }

    public void CloseRevealUI()
    {
        if (revealC != null)
            StopCoroutine(revealC);

        revealC = StartCoroutine(FadeOutCanvasGroup(1, 0, 0.5f, revealCanvasGroup));
        AudioController.Instance.PlayRegularGameSound();
    }

    private IEnumerator FadeInCanvasGroup(float from, float to, float duration, CanvasGroup canvasGroup)
    {
        canvasGroup.gameObject.SetActive(true);
        float elapsedTime = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    private IEnumerator FadeOutCanvasGroup(float from, float to, float duration, CanvasGroup canvasGroup)
    {
        float elapsedTime = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }

        canvasGroup.alpha = to;
        canvasGroup.gameObject.SetActive(false);
        revealUi.SetActive(false);
    }
}
