using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneController : MonoBehaviour
{
    public GameObject[] scenes;
    private int sceneIndex = 0;

    public GameObject backgroundMusicChill;
    public GameObject backgroundMusicStress;
    public GameObject backgroundMusicEnd;

    public void ContinueToNextScenePart()
    {
        scenes[sceneIndex].SetActive(false);
        sceneIndex++;
        scenes[sceneIndex].SetActive(true);

        if (sceneIndex == 5)
        {
            backgroundMusicChill.SetActive(false);
            backgroundMusicStress.SetActive(true); 
        } else if (sceneIndex == 10)
        {
            backgroundMusicChill.SetActive(false);
            backgroundMusicStress.SetActive(false);
            backgroundMusicEnd.SetActive(true);
        }

        
    }
}
