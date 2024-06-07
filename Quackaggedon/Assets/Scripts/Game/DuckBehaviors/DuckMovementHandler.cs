using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DuckMovementHandler : MonoBehaviour
{
    public Animator duckAnim;
    public GameObject duckHolder;
    public Vector3 randomPosition;
    public float maxSwimSpeed = 1;
    public float minSwimSpeed = 0.1f;
    public float decelerationRadius = 2.0f;  // The radius within which the duck starts decelerating
    public float minWaitTime = 0.5f;  // Minimum wait time before changing position
    public float maxWaitTime = 2.0f;  // Maximum wait time before changing position
    public float stopTimeAfterClick = 0.5f;  // Time to stop after being clicked
    public float forcefieldRadius = 1.0f;
    public float forcefieldMover;
    public float forcefieldMultiplier;

    private bool isWaiting = false;
    public static float lastClickTime = 0;
    public static Transform lastClickedDuck; 

    private void Start()
    {
        randomPosition = Common.Instance.GetRandomPositionWithinPond(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastClickedDuck != null &&
            lastClickedDuck != transform &&
            Time.timeSinceLevelLoad - lastClickTime < stopTimeAfterClick)
        {
            float distance = Vector2.Distance(transform.position, lastClickedDuck.position);
            if (distance < forcefieldRadius)
            {
                Vector2 dir = (transform.position - lastClickedDuck.position).normalized;
                float force = (1 / (distance + forcefieldMover) * forcefieldMultiplier) - forcefieldMover;
                transform.position += (Vector3)dir * (Time.deltaTime * force);
            }
        }
        
        if (!isWaiting && lastClickedDuck != transform)
        {
            if (Time.timeSinceLevelLoad - lastClickTime > stopTimeAfterClick)
                lastClickedDuck = null;
            
            duckAnim.SetBool("Swim", true);
            SmoothRandomMovement();
        } else
        {
            duckAnim.SetBool("Swim", false);
        }
    }

    public void SmoothRandomMovement()
    {
        float distance = Vector2.Distance(transform.position, randomPosition);
        var speedModifier = (distance + UnityEngine.Random.Range(0,2))/ 3;

        // Smoothly decelerate as the duck gets closer to the target
        float speed = Mathf.Lerp(minSwimSpeed, maxSwimSpeed, distance / decelerationRadius) * speedModifier;
        speed = Mathf.Clamp(speed, minSwimSpeed, maxSwimSpeed);

        if (distance < 0.5f)
        {
            StartCoroutine(WaitAndChangePosition());
        }
        else
        {
            Vector2 dir = (randomPosition - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, randomPosition, speed * Time.deltaTime);
            FlipToMovementDirection(dir);
        }
    }

    private IEnumerator WaitAndChangePosition()
    {
        isWaiting = true;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        randomPosition = Common.Instance.GetRandomPositionWithinPond(transform.position);
        duckAnim.SetTrigger("Flip");
        isWaiting = false;
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
