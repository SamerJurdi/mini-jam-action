using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D myRBody;
    public float mySpeed = 6.0f;
    [Header("Projectile")]
    public GameObject LaserPrefab;
    public float ShotInterval;
    public float ShotSpeed;
    public float ShotSpawningDistanceY;
    public int earthDamage = 1;
    public int maxShipHealth = 5;
    private int shipHealth;
    public int ScoreValue = 100;
    public GameObject LaserImpactPrefab;
    public GameObject ShipDeathVFXPrefab;
    public List<GameObject> pickupsPrefab;
    public float chanceToDropPickupOnDeath;
    public SpriteRenderer myGFX;
    private float spawnTimeStamp;
    private float lastDamagedTimeStamp;
    private float flashForDamageTime = 0.1f;

    void Start()
    {
        myRBody = GetComponent<Rigidbody2D>();
        myRBody.velocity = Vector2.down *mySpeed;

        shipHealth = maxShipHealth;
        spawnTimeStamp = Time.time;
    }

    void Update()
    {
        if ((Time.time - spawnTimeStamp) % ShotInterval < Time.deltaTime)
        {
            // fire a shot
            GameObject temp = Instantiate(LaserPrefab, new Vector3(this.myRBody.transform.position.x, this.myRBody.transform.position.y + ShotSpawningDistanceY, 0.0f), Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,ShotSpeed);
        }

        if (Time.time - lastDamagedTimeStamp < flashForDamageTime)
        {
            myGFX.color = Color.red;
        }
        else
            myGFX.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerLaser") || collision.gameObject.CompareTag("PlayerSuperLaser"))
        {
            // take damage
            if (collision.gameObject.CompareTag("PlayerSuperLaser"))
                shipHealth = 0;
            else
                shipHealth--;

            lastDamagedTimeStamp = Time.time;
            //Destroy Laser
            if (!collision.gameObject.CompareTag("PlayerSuperLaser"))
            {
                Destroy(collision.gameObject);
                Instantiate(LaserImpactPrefab, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, 0.0f), collision.transform.rotation);
            }


            // if out of health, blow up ship
            if (shipHealth <= 0)
            {
                GameManager.GM.AddScore(ScoreValue);
                DestroyWithEffect();
            }
        }
    }

    public void DestroyWithEffect(bool withFX = true)
    {
        Debug.Log("Destroying Enemy");
        if (withFX)
        {
            Instantiate(ShipDeathVFXPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
            if(Random.Range(0,1f) < chanceToDropPickupOnDeath)
            {
                int pickupIndex = 0;
                if (Random.Range(0, 100f) < 75f)
                    pickupIndex = 1;

                GameObject temp = Instantiate(pickupsPrefab[pickupIndex], new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().velocity = myRBody.velocity;
            }
        }
        Destroy(gameObject);
    }
}
