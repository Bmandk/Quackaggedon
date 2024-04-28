using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IntroSceneHandler : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public void LoadGameScene()
    {
        sceneHandler.LoadNewScene(SceneHandler.Scene.GameScene, SceneHandler.Scene.Intro);
    }
}
