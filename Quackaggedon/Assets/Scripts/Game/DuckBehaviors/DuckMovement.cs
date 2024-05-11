using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public float speed = 1.0f; // Movement speed of the duck
    public float changeDirectionTime = 2.0f; // Minimum time between direction changes (in seconds)
    public float maxChangeDirectionTime = 5.0f; // Maximum time between direction changes (in seconds)

    private Rigidbody2D rb;
    private Vector2 movementDirection; // Current movement direction
    private float nextDirectionChangeTime; // Time of the next direction change

    private Collider2D pondCollider; // Reference to the pond's polygon collider

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pondCollider = References.Instance.pondCollider; // Get pond collider reference

        // Set initial random movement direction
        movementDirection = Random.insideUnitCircle.normalized;

        // Set the time for the next direction change
        nextDirectionChangeTime = Time.time + Random.Range(changeDirectionTime, maxChangeDirectionTime);
    }

    void FixedUpdate()
    {
        // Check if it's time to change direction
        if (Time.time >= nextDirectionChangeTime)
        {
            movementDirection = Random.insideUnitCircle.normalized;
            nextDirectionChangeTime = Time.time + Random.Range(changeDirectionTime, maxChangeDirectionTime);
        }

        // Move the duck within the pond collider
        Vector3 m = movementDirection;
        Vector3 newPosition = transform.position + m * speed * Time.deltaTime;
        var x = Mathf.Clamp(newPosition.x, pondCollider.bounds.min.x, pondCollider.bounds.max.x);
        var y = Mathf.Clamp(newPosition.y, pondCollider.bounds.min.y, pondCollider.bounds.max.y);
        var z = 0;
        Vector3 clampedPosition = new Vector3(x,y,z);//Mathf.Clamp(newPosition, pondCollider.bounds.min, pondCollider.bounds.max);
        rb.MovePosition(clampedPosition);
    }
}
