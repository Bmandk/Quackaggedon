using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayQuack : MonoBehaviour
{
    public void PlayRandomQuack()
    {
        MainMenuUISoundController.Instance.PlayRandomQuack();
    }

    public void PlayRandomTap()
    {
        MainMenuUISoundController.Instance.PlayRandomTap();
    }

    public void PlayGrassRustle1()
    {
        MainMenuUISoundController.Instance.PlayGrass1();
    }

    public void PlayGrassRustle2()
    {
        MainMenuUISoundController.Instance.PlayGrass2();
    }

    public void PlayHiss()
    {
        MainMenuUISoundController.Instance.PlayHiss();
    }

    public void PlayMunchFoodSound()
    {
        MainMenuUISoundController.Instance.PlayMunchFoodSound();
    }
}
