using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class DuckMovementHandler : MonoBehaviour
{
    //public float speed;
    public GameObject duckHolder;
    public Vector3 randomPosition;

    //public float relaxSwimSpeed = 0.8f;
    public float maxSwimSpeed = 1;

    private Vector2 worldPosition;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
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
        if (Vector2.Distance(transform.position, randomPosition) < 0.5)
        {
            randomPosition = Common.Instance.RandomPos(transform.position);
        }

       // Debug.Log(randomPosition);

        Vector2 dir = (randomPosition - transform.position).normalized;

        rb.AddRelativeForce(dir * Common.Instance.duckRelaxSpeed);//relaxSwimSpeed);
        FlipToMovementDirection();
    }

    private void MoveDuckToCursor()
    {

        {
            //this.transform.position = worldPosition;   

            var step = Common.Instance.duckGetFoodSpeed * Time.deltaTime;

            Vector2 pos = transform.position;
            Vector2 dir = (worldPosition - pos).normalized;

            if (rb.velocity.magnitude > maxSwimSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSwimSpeed;
                //rb.AddRelativeForce(-dir * speed);
            }
            else
            {
                rb.AddRelativeForce(dir * Common.Instance.duckGetFoodSpeed * (Vector3.Distance(transform.position, worldPosition) / 2));
            }
            FlipToMovementDirection();
        }
    }

    private void FlipToMovementDirection()
    {
        if (rb.velocity.x < 0)
        {
            duckHolder.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            duckHolder.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void HighlightDuck()
    {
        //littleFish.GetComponent<SpriteRenderer>().material = Common.Instance.highlightMat;
        //bigFish.GetComponent<SpriteRenderer>().material = Common.Instance.highlightMat;
        //References.Instance.uiHandler.TurnOnFishInfo(UserInput.clickedFish.GetComponent<FishInstanceData>().FishId);
    }

    public void UnHighlightDuck()
    {
        /*
        if (!Common.IsPointerOverUIObject())
        {
            littleFish.GetComponent<SpriteRenderer>().material = Common.Instance.defaultMat;
            bigFish.GetComponent<SpriteRenderer>().material = Common.Instance.defaultMat;
            References.Instance.uiHandler.TurnOffFishInfo();
        }
        */
    }
}
