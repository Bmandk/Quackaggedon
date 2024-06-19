using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class RevealHandler : MonoBehaviour
{
    private static RevealHandler _instance;

    public static RevealHandler Instance
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


    [SerializeField]
    private GameObject cleverBoon, magicBoon, breadBoon, chefBoon;

    public static bool revealIsActive = false;

    private static DuckType duckBeingRevealed;

    private Coroutine revealC;

    private Action ToDoAfter;

    private bool pressedCloseReveal = false;

    public void AddActionToAfterReveal(Action ToDoAfter)
    { 
        this.ToDoAfter = ToDoAfter;
    }
    public void ShowRevealUI(DuckData duckToShow)
    {
        revealIsActive = true;
        duckBeingRevealed = duckToShow.duckType;

        revealDuckName.text = $"<wave a=0.1>{duckToShow.duckDisplayName}</wave>";
        revealDuckSkillText.text = duckToShow.duckEffectDescription;
        revealDuckIcon.sprite = duckToShow.duckDisplayIconRevealed;
        revealCanvasGroup.alpha = 0;

        cleverBoon.SetActive(false);
        magicBoon.SetActive(false);
        breadBoon.SetActive(false);
        chefBoon.SetActive(false);
        switch (duckToShow.duckType)
        {
            case DuckType.Chef:
                chefBoon.SetActive(true);
                break;
            case DuckType.Bread:
                breadBoon.SetActive(true);
                break;
            case DuckType.Magical:
                magicBoon.SetActive(true);
                break;
            case DuckType.Clever:
                cleverBoon.SetActive(true);
                break;
            default:
                break;
        }

        revealUi.SetActive(true);

        if (revealC != null)
            StopCoroutine(revealC);
        revealC = StartCoroutine(FadeInCanvasGroup(0, 1, 0.5f, revealCanvasGroup));
    }

    public void CloseRevealUI()
    {
        if (!pressedCloseReveal)
        {
            pressedCloseReveal = true;
            if (revealC != null)
                StopCoroutine(revealC);

            revealC = StartCoroutine(FadeOutCanvasGroup(1, 0, 0.5f, revealCanvasGroup));
            AudioController.Instance.PlayRegularGameSound();
        }
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

        yield return new WaitForSeconds(1);

        canvasGroup.gameObject.SetActive(false);
        if (ToDoAfter != null)
        {
            ToDoAfter.Invoke();
            AudioController.Instance.PlayRevealNewFoodButtonSound();
        }

        References.Instance.sceneDataHolder.EquipAllDucksWithUpgrade(duckBeingRevealed);

        revealIsActive = false;
        pressedCloseReveal = false;
        revealUi.SetActive(false);
    }
}
