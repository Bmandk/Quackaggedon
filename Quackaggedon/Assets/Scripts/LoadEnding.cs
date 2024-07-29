using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEnding : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public void LoadWinScene()
    {
        sceneLoader.LoadNewScene(SceneLoader.Scene.End, SceneLoader.Scene.GameScene);
    }
}
