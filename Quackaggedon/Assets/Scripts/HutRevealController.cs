using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HutRevealController : MonoBehaviour
{
    public GameObject hutUI;

    public static float level1HutDuckPercentage = 0.3f;
    public static float level2HutDuckPercentage = 0.6f;
    public static float level3HutDuckPercentage = 1f;
    public static float maxLevelDuckAmount = 140;

    public GameObject[] duckIconsOnHut;
    public CanvasGroup roofDirty;
    public CanvasGroup wallDirty;
    public CanvasGroup groundDirty;

    public GameObject roofGlow;
    public GameObject wallGlow;
    public GameObject groundGlow;

    public Material defaultMat;
    public Material glowMat;

    public static bool revealedLvl1;
    public static bool revealedLvl2;
    public static bool revealedLvl3;
    public static int amountOfDuckIconsAlreadyRevealed;

    private int ducksSavedToHut;
    public static int duckIconsThatShouldBeRevealed;
    private float waitUntilRevealUpdate = 0.6f;

    public static void ResetHutValues()
    {
        revealedLvl1 = false;
        revealedLvl2 = false;
        revealedLvl3 = false;
        amountOfDuckIconsAlreadyRevealed = 0;
        duckIconsThatShouldBeRevealed = 0;
    }

    public void RevealHutContentsCorrectly()
    {
        ducksSavedToHut = (int)DuckAmounts.GetTotalDucksInHut();
        duckIconsThatShouldBeRevealed = (int) (Mathf.Min((ducksSavedToHut / maxLevelDuckAmount),1) * duckIconsOnHut.Length);

        RevealDucks();
        RevealCleanHutParts();

        hutUI.SetActive(true);
    }

    public void UpdateUIIfNewDucksInHut()
    {
        if (DuckAmounts.GetTotalDucksInHut() != ducksSavedToHut)
        {
            RevealHutContentsCorrectly();
        }
    }

    private void RevealCleanHutParts()
    {
        if (revealedLvl1)
        {
            groundDirty.alpha = 0;
        }
        else
        {
            groundDirty.alpha = 1;
            if (ducksSavedToHut >= (level1HutDuckPercentage * maxLevelDuckAmount))
            {
                StartCoroutine(SlowRevealClean(groundDirty,0.6f));
                StartCoroutine(GlowHutPart(groundGlow));
                revealedLvl1 = true;
            }
        }

        if (revealedLvl2) 
        {
            wallDirty.alpha = 0;
        }
        else
        {
            wallDirty.alpha = 1;
            if (ducksSavedToHut >= (level2HutDuckPercentage*maxLevelDuckAmount))
            {
                StartCoroutine(SlowRevealClean(wallDirty, 0.6f));
                StartCoroutine(GlowHutPart(wallGlow));
                revealedLvl2 = true;
            }
        }

        if (revealedLvl3)
        {
            roofDirty.alpha = 0;
        }
        else
        {
            roofDirty.alpha = 1;
            if (ducksSavedToHut >= (level3HutDuckPercentage * maxLevelDuckAmount))
            {
                StartCoroutine(SlowRevealClean(roofDirty, 0.6f));
                StartCoroutine(GlowHutPart(roofGlow));
                revealedLvl3 = true;
            }
        }
    }

    private void RevealDucks()
    {
        StartCoroutine(DisplayDucksCorrectly(duckIconsThatShouldBeRevealed));
    }

    private IEnumerator SlowRevealClean(CanvasGroup cv, float fadeDuration)
    {
        yield return new WaitForSeconds(waitUntilRevealUpdate);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cv.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }
    }

    IEnumerator DisplayDucksCorrectly(int ducksToReveal)
    {
        foreach (var duck in duckIconsOnHut)
        {
            duck.gameObject.SetActive(false);
        }

        for (int i = 0; i < amountOfDuckIconsAlreadyRevealed; i++)
        {
            duckIconsOnHut[i].GetComponent<Image>().material = defaultMat;
            duckIconsOnHut[i].gameObject.SetActive(true);
        }

        for (int i = amountOfDuckIconsAlreadyRevealed; i < duckIconsOnHut.Length; i++)
        {
            duckIconsOnHut[i].GetComponent<Image>().material = defaultMat;
            duckIconsOnHut[i].gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(waitUntilRevealUpdate);

        AudioController.Instance.PlayRandomQuack();

        for (int i = amountOfDuckIconsAlreadyRevealed; i < ducksToReveal; i++)
        {
            duckIconsOnHut[i].GetComponent<Image>().material = glowMat;
            duckIconsOnHut[i].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = amountOfDuckIconsAlreadyRevealed; i < ducksToReveal; i++)
        {
            duckIconsOnHut[i].GetComponent<Image>().material = defaultMat;
            duckIconsOnHut[i].gameObject.SetActive(true);
        }

        amountOfDuckIconsAlreadyRevealed = ducksToReveal;
    }

    IEnumerator GlowHutPart(GameObject glowOj)
    {
        yield return new WaitForSeconds(waitUntilRevealUpdate);

        AudioController.Instance.PlayBloopSound();
        glowOj.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        glowOj.SetActive(false);
    }

    public void CloseHutUI()
    {
        hutUI.SetActive(false);
    }
}
