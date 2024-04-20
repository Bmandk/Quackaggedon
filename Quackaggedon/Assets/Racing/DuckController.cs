using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DuckController : MonoBehaviour
    {
        public float acceleration = 1.0f;
        public float maxSpeed = 5.0f;
        public float rotationSpeed = 1.0f;
        public float maxRotationSpeed = 5.0f;
        public float rotationDecay = 0.1f;
    
        private Rigidbody2D rb;
        private float rotation = 0.0f;
        private Vector2 velocity;
    
    
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
    
        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
        
            if (horizontal != 0.0f)
            {
                rotation += horizontal * rotationSpeed * Time.deltaTime;
                rotation = Mathf.Clamp(rotation, -maxRotationSpeed, maxRotationSpeed);
            }
            else
            {
                rotation = Mathf.Lerp(rotation, 0.0f, Time.deltaTime * rotationDecay);
            }
        
            if (vertical != 0.0f)
            {
                rb.velocity += (Vector2)transform.right * (vertical * acceleration * Time.deltaTime);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }
        
            rb.MoveRotation(rb.rotation - rotation);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Toast"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}