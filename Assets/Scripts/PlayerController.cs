using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float tiltAngle = 45f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Boundaries Configuration")]
    [Tooltip("Upper boundary in world units")]
    public float maxY = -2f;
    [Tooltip("Lower boundary in world units")]
    public float minY = -4.5f;

    [Header("Shooting")]
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public float laserSpeed = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        UpdateRotation(movement.x);
        HandleShooting();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyLaser") || collision.gameObject.CompareTag("Enemy"))
        {
            HandleEnemyCollision();
        }
    }

    private void HandleEnemyCollision()
    {
        Debug.Log("Handling enemy collision...");
        Destroy(gameObject);
        gameManager.PlayerDied();
        // TODO: Add Destruction/Damage Animation
        // TODO: Add Destruction/Damage SFX
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: Play Laser shot SFX
            ShootLaser();
        }
    }

    private void ShootLaser()
    {
        if (laserPrefab != null && laserSpawnPoint != null)
        {
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, transform.rotation);
            Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
            if (laserRb != null)
            {
                laserRb.velocity = transform.up * laserSpeed;
            }
        }
    }
}
