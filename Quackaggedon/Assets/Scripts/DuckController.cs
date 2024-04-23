using System.Collections;
using UnityEngine;

namespace DuckClicker
{
    public class DuckController : MonoBehaviour
    {
        public AnimationCurve startGrowingCurve;
        public float targetScale = 1.0f;
        
        void Start()
        {
            StartCoroutine(GrowDuck());
        }

        public IEnumerator GrowDuck()
        {
            float time = 0.0f;

            while (time < startGrowingCurve.keys[startGrowingCurve.length - 1].time)
            {
                time += Time.deltaTime;
                float scale = startGrowingCurve.Evaluate(time) * targetScale;
                transform.localScale = new Vector3(scale, scale, 1.0f);
                yield return null;
            }
        }
    }
}