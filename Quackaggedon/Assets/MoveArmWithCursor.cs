using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArmWithCursor : MonoBehaviour
{

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public float offset;
    public float maxY;
    public GameObject arm;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(1))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 newPos = new Vector3(mousePosition.x, Mathf.Min(mousePosition.y + offset, maxY), 0);//arm.transform.position.y, 0);
            arm.transform.position = newPos;
        }

    }
}
