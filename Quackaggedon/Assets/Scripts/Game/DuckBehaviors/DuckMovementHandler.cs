using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    public bool flyingToHut;
    private bool startedFlying;

    private Vector3 targetPosition; // The target position in world space
    private float moveDuration = 3.5f; // Duration to move
    private float elapsedTime = 0f; // Track time elapsed

    private void Start()
    {
        randomPosition = Common.Instance.GetRandomPositionWithinPond(transform.position);
        //InvokeRepeating(nameof(Quack), 1, 1);

        // Convert the UI position to world space position
        var hutUI = References.Instance.menuController.hutButtonFlyPoint;
        //Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, hutUI.position);
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(hutUI, screenPoint, Camera.main, out targetPosition);
    }

    private void Quack()
    {
        duckAnim.SetTrigger("Quack");
    }

    // Update is called once per frame
    void Update()
    {
        if (!flyingToHut && !startedFlying)
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
            }
            else
            {
                duckAnim.SetBool("Swim", false);
            }
        }
        else if (flyingToHut && !startedFlying)
        {
            startedFlying = true;
            StopAllCoroutines();
            duckAnim.SetBool("Fly", true);
            AudioController.Instance.PlayWingFlap();
            duckHolder.transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("Flying!");
        } 
        else if (startedFlying)
        {
            if (elapsedTime < moveDuration)
            {
                // Calculate the interpolation factor
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / moveDuration;

                // Apply quadratic ease-in-out
                if (t < 1.3f)
                {
                    t = 2 * t * t;
                }
                else
                {
                    t = -1 + (4 - 2 * t) * t;
                }

                var towards = Camera.main.ScreenToWorldPoint(References.Instance.menuController.hutButtonFlyPoint.position);
                // Linearly interpolate the position of the GameObject towards the target
                transform.position = Vector3.Lerp(transform.position, towards, t);

                if (Vector3.Distance(transform.position, towards) < 0.7f)
                {
                    References.Instance.menuController.BigPulseHutButton();
                    Destroy(gameObject); // Destroy the GameObject once it reaches the target
                }

                // Check if the GameObject has reached the target position
                if (t >= 1.0f)
                {
                    Destroy(gameObject); // Destroy the GameObject once it reaches the target
                }
            }
        }
    }

    public void SmoothRandomMovement()
    {
        float distance = Vector2.Distance(transform.position, randomPosition);
        var speedModifier = (distance + UnityEngine.Random.Range(0, 2)) / 3;

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

    public void FlyToHut()
    {
        flyingToHut = true;
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
