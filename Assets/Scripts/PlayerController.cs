using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float tiltAngle = 45f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Boundaries Configuration")]
    [Range(0f, 1f)]
    public float maxHeight = 0.3f;
    private float screenHeight;
    private float minY;
    private float maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calculate screen boundaries
        screenHeight = Camera.main.orthographicSize * 2f;
        minY = -screenHeight / 2;
        maxY = minY + screenHeight * maxHeight;
    }

    void Update()
    {
        UpdateRotation(movement.x);
    }

    void FixedUpdate()
    {
        UpdateMovement();
        EnforceBoundary();
    }

    private void UpdateMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.velocity = movement.normalized * moveSpeed;
    }

    private void UpdateRotation(float horizontalMovement)
    {
        if (horizontalMovement < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, tiltAngle); // Tilt left
        }
        else if (horizontalMovement > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -tiltAngle); // Tilt right
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void EnforceBoundary()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }
}
