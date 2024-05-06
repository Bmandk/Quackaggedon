using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArmWithCursor : MonoBehaviour
{

    private Vector3 mousePosition;
    public float offset;
    public float maxY;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(1))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 newPos = new Vector3(mousePosition.x, Mathf.Min(mousePosition.y + offset, maxY), 0);
            transform.position = newPos;
        }

    }
}
