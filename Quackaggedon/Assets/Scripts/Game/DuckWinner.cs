﻿using System.Collections;
using UnityEngine;

namespace DuckClicker
{
    public class DuckWinner : MonoBehaviour
    {
        public float winTime = 10.0f;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(winTime);
            SceneLoader sceneHandler = FindObjectOfType<SceneLoader>();
            sceneHandler.LoadNewScene(SceneLoader.Scene.WinScreen, SceneLoader.Scene.GameScene);
        }
    }
}