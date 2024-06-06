using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHandler : MonoBehaviour
{

    private Vector3 mousePosition;
    public float offset;
    public float maxY;
    public float minY;

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float yMax = Mathf.Min(mousePosition.y + offset);
        float yFinal = Mathf.Max(minY, yMax);
        Vector3 newPos = new Vector3(mousePosition.x, Mathf.Min(-0.96f, yFinal, maxY), 0);

        transform.position = newPos;
    }

}
