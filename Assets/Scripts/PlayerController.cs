using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Managed by the game manager to give immunity on spawn")]
    public bool isPlayerActive = true;
    private GameManager gameManager;
    private UIManager uiManager;

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
    [Tooltip("Right boundary in world units")]
    public float maxX = 8.8f;
    [Tooltip("Left boundary in world units")]
    public float minX = -8.8f;

    [Header("Shooting")]
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public float laserSpeed = 20f;
    public GameObject missilePrefab;
    public float missileSpeed = 20f;
    public GameObject LaserImpactPrefab;
    public GameObject ShipDeathVFXPrefab;
    public bool hasMissile = true;

    [Header("Pickups")]
    public AudioClip HealthPickupSound;
    public AudioClip MissilePickupSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        uiManager = GameObject.FindWithTag("UIController").GetComponent<UIManager>();
        setMissileInStock(hasMissile);
    }

    void Update()
    {
        if (isPlayerActive)
        {
            UpdateRotation(movement.x);
            HandleShooting();
        }
    }

    void FixedUpdate()
    {
        if (isPlayerActive)
        {
            UpdateMovement();
            EnforceBoundary();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObj = collision.gameObject;
        if (collidedObj.CompareTag("EnemyLaser") && isPlayerActive)
        {
            // destroy that laser and spawn in laser impact VFX
            Destroy(collidedObj);
            if (collidedObj.CompareTag("EnemyLaser"))
                Instantiate(LaserImpactPrefab, new Vector3(collidedObj.transform.position.x, collidedObj.transform.position.y, 0.0f), Quaternion.identity);
            HandleEnemyCollision();
        }
        if (collidedObj.CompareTag("Enemy") && isPlayerActive)
        {
            collidedObj.GetComponent<Enemy>().DestroyWithEffect();
            HandleEnemyCollision();
        }
        if (collidedObj.CompareTag("HealthPickup"))
        {
            GameManager.GM.PlaySound(HealthPickupSound);
            GameManager.GM.HealEarth(1);
            Destroy(collidedObj);
        }
        if (collidedObj.CompareTag("MissilePickup"))
        {
            GameManager.GM.PlaySound(MissilePickupSound);
            setMissileInStock(true);
            Destroy(collidedObj);
        }
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
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }

    private void HandleEnemyCollision()
    {
        Debug.Log("Handling enemy collision...");
        Destroy(gameObject);
        Instantiate(ShipDeathVFXPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        gameManager.PlayerDied();
    }

    private void HandleShooting()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootLaser();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (hasMissile)
                    ShootMissile();
            }
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

    private void ShootMissile()
    {
        if (missilePrefab != null && laserSpawnPoint != null)
        {
            GameObject laser = Instantiate(missilePrefab, laserSpawnPoint.position, transform.rotation);
            Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
            if (laserRb != null)
            {
                laserRb.velocity = transform.up * missileSpeed;
            }
            setMissileInStock(false);
        }
    }

    private void setMissileInStock(bool inStock)
    {
        hasMissile = inStock;
        uiManager.ToggleMissileIcon(inStock);
    }
}
