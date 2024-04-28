using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IntroSceneHandler : MonoBehaviour
{
    public SceneLoader sceneHandler;
    public void LoadGameScene()
    {
        sceneHandler.LoadNewScene(SceneLoader.Scene.GameScene, SceneLoader.Scene.Intro);
    }
}
