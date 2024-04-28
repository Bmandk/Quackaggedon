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
}
