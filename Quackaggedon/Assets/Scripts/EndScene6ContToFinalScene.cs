using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene6ContToFinalScene : MonoBehaviour
{
    public EndStoryBoardHandler endStoryBoardHandler;

    public void ContinueStory()
    {
        endStoryBoardHandler.ContinueStoryBoard();
    }
}
