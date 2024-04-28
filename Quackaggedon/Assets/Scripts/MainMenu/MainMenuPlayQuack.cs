using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayQuack : MonoBehaviour
{
    public void PlayRandomQuack()
    {
        MainMenuUISoundHandler.Instance.PlayRandomQuack();
    }

    public void PlayRandomTap()
    {
        MainMenuUISoundHandler.Instance.PlayRandomTap();
    }

    public void PlayGrassRustle1()
    {
        MainMenuUISoundHandler.Instance.PlayGrass1();
    }

    public void PlayGrassRustle2()
    {
        MainMenuUISoundHandler.Instance.PlayGrass2();
    }

    public void PlayHiss()
    {
        MainMenuUISoundHandler.Instance.PlayHiss();
    }
}
