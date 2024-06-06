using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHandler : MonoBehaviour
{

    private Vector3 mousePosition;
    public float offset;
    public float maxY;
    public float minY;

    //public static bool isThrowing;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float yMax = Mathf.Min(mousePosition.y + offset);
        float yProper = Mathf.Max(minY, yMax);
        Vector3 newPos = new Vector3(mousePosition.x, Mathf.Min(-0.96f, yProper, maxY), 0);

        // if (isThrowing)
        transform.position = newPos;//new Vector3(newPos.x, -0.96f, 0);
       // else
       //     transform.position = newPos;
    }

}
