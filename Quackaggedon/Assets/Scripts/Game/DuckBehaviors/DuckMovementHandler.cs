using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovementHandler : MonoBehaviour
{
    public GameObject duckHolder;
    public Vector3 randomPosition;

    public float maxSwimSpeed = 1;

    private Vector2 worldPosition;

    private void Start()
    {
        randomPosition = Common.Instance.RandomPos(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = References.Instance.mainCam.nearClipPlane;
        worldPosition = References.Instance.mainCam.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButton(0) && Vector2.Distance(transform.position, worldPosition) < Common.Instance.maxDistanceToFood)
        {
            MoveDuckToCursor();
        }
        else
        {
            SmoothRandomMovement();
        }
    }

    public void SmoothRandomMovement()
    {
        if (Vector2.Distance(transform.position, randomPosition) < 0.5f)
        {
            randomPosition = Common.Instance.RandomPos(transform.position);
        }

        Vector2 dir = (randomPosition - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, randomPosition, Common.Instance.duckRelaxSpeed * Time.deltaTime);
        FlipToMovementDirection(dir);
    }

    private void MoveDuckToCursor()
    {
        var step = Common.Instance.duckGetFoodSpeed * Time.deltaTime;
        Vector2 dir = (worldPosition - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, worldPosition, step);
        FlipToMovementDirection(dir);
    }

    private void FlipToMovementDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            duckHolder.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            duckHolder.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
