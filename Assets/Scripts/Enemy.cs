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
    public GameObject LaserImpactPrefab;
    public GameObject ShipDeathVFXPrefab;

    void Start()
    {
        myRBody = GetComponent<Rigidbody2D>();
        myRBody.velocity = Vector2.down *mySpeed;
    }

    void Update()
    {
        if (Time.time % ShotInterval < Time.deltaTime)
        {
            // fire a shot
            GameObject temp = Instantiate(LaserPrefab, new Vector3(this.myRBody.transform.position.x, this.myRBody.transform.position.y + ShotSpawningDistanceY, 0.0f), Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,ShotSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerLaser") || collision.gameObject.CompareTag("Player"))
        {
            //Destroy Laser
            Destroy(collision.gameObject);
            if (collision.gameObject.CompareTag("PlayerLaser"))
                Instantiate(LaserImpactPrefab, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, 0.0f), collision.transform.rotation);

            CleanUpAndDestroy();
        }
    }

    public void CleanUpAndDestroy()
    {
        Debug.Log("Destroying Enemy");
        Instantiate(ShipDeathVFXPrefab, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        Destroy(gameObject);
    }
}
