using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public Transform target;
    //public Vector2 offset;
    public bool isWorldPosition;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPos = target.position;
            if (isWorldPosition)
            {
                targetPos = Camera.main.WorldToScreenPoint(targetPos);
            }

            transform.position = targetPos/* + (Vector3)offset*/;
        }
    }
}
