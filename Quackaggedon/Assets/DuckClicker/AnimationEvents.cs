using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DuckClicker
{
    public class AnimationEvents : MonoBehaviour
    {
        public UnityEvent Event1;

        public void OnEvent1()
        {
            Event1.Invoke();
        }
    }
}