using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePausContinuer : MonoBehaviour
{
    public Animator animator;
    public EndStoryBoardHandler endStoryBoardHandler;

    public void ContinueToNextScene()
    {
        if (animator.GetBool("LoadHappyWalk"))
        {
            //Load happy walk
        } else
        {
            animator.SetTrigger("FadeOut");
            endStoryBoardHandler.ContinueStoryBoard();
        }
    }

    public void TurnOffFader()
    {
        this.gameObject.SetActive(false);
    }
}
