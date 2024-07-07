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
    public static float maxLevelDuckAmount = 260;

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
    private float flashRevealTime = 0.2f;

    public HutAnimSetValues animSetValues;
    public HutSliderAnim hutSliderAnim;

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
        int duckIconsToShow = (int)(Mathf.Min((ducksSavedToHut / maxLevelDuckAmount), 1) * duckIconsOnHut.Length);

        if (duckIconsToShow == 0 && ducksSavedToHut > 0)
        {
            duckIconsThatShouldBeRevealed = 1;
        }
        else
        {
            duckIconsThatShouldBeRevealed = duckIconsToShow;
        }

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
        if (!revealedLvl1)
        {
            if (ducksSavedToHut >= (level1HutDuckPercentage * maxLevelDuckAmount))
            {
                RevealLevel1();
                revealedLvl1 = true;
            }
        } 
        else
        {
            if (hutUI.activeSelf) 
            {
                animSetValues.SetGroundToCleaned();
                animSetValues.SetLibraryToShown();
                animSetValues.SetPillowToShown();
            }
        }

        if (!revealedLvl2) 
        {
            if (ducksSavedToHut >= (level2HutDuckPercentage*maxLevelDuckAmount))
            {
                RevealLevel2();
                revealedLvl2 = true;
            }
        }
        else
        {
            if (hutUI.activeSelf)
            {
                animSetValues.SetWallToCleaned();
                animSetValues.SetTVToShown();
            }
        }

        if (!revealedLvl3)
        {
            if (ducksSavedToHut >= (level3HutDuckPercentage * maxLevelDuckAmount))
            {
                RevealLevel3();
                revealedLvl3 = true;
            }
        }
        else
        {
            if (hutUI.activeSelf)
            {
                animSetValues.SetRoofToCleaned();
                animSetValues.SetMagicalCircleToShown();
                animSetValues.SetShroomsToShown();
            }
        }
    }

    private void RevealDucks()
    {
        StartCoroutine(DisplayDucksCorrectly(duckIconsThatShouldBeRevealed));
    }

    /*
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
    */

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

        yield return new WaitForSeconds(flashRevealTime);

        for (int i = amountOfDuckIconsAlreadyRevealed; i < ducksToReveal; i++)
        {
            duckIconsOnHut[i].GetComponent<Image>().material = defaultMat;
            duckIconsOnHut[i].gameObject.SetActive(true);
        }

        amountOfDuckIconsAlreadyRevealed = ducksToReveal;
    }

    /*
    IEnumerator GlowHutPart(GameObject glowOj)
    {
        yield return new WaitForSeconds(waitUntilRevealUpdate);

        AudioController.Instance.PlayBloopSound();
        glowOj.SetActive(true);

        yield return new WaitForSeconds(flashRevealTime);

        glowOj.SetActive(false);
    }
    */

    public void CloseHutUI()
    {
        hutUI.SetActive(false);
    }

    private void RevealLevel1()
    {
        hutSliderAnim.PulseSlider();
        StartCoroutine(Reveal1());
    }

    private void RevealLevel2()
    {
        hutSliderAnim.PulseSlider();
        StartCoroutine(Reveal2());
    }

    public void RevealLevel3()
    {
        hutSliderAnim.PulseSlider();
        StartCoroutine(Reveal3());
    }

    private IEnumerator Reveal1()
    {
        yield return new WaitForSeconds(waitUntilRevealUpdate);
        animSetValues.CleanGround();
        animSetValues.ShowLibrary();
        animSetValues.ShowPillow();
    }

    private IEnumerator Reveal2()
    {
        yield return new WaitForSeconds(waitUntilRevealUpdate);
        animSetValues.CleanWall();
        animSetValues.ShowTV();
    }

    private IEnumerator Reveal3()
    {
        yield return new WaitForSeconds(waitUntilRevealUpdate);
        animSetValues.CleanRoof();
        animSetValues.ShowMagicCircle();
        animSetValues.ShowShrooms();
    }

}
