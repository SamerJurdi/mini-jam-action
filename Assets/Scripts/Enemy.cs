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
    // Start is called before the first frame update
    void Start()
    {
        myRBody = GetComponent<Rigidbody2D>();
        myRBody.velocity = Vector2.down *mySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time % ShotInterval < Time.deltaTime)
        {
            // fire a shot
            GameObject temp = Instantiate(LaserPrefab, new Vector3(this.myRBody.transform.position.x, this.myRBody.transform.position.y + ShotSpawningDistanceY, 0.0f), Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,ShotSpeed);
        }
    }

    public void CleanUpAndDestroy()
    {
        Debug.Log("Destroying Enemy");
        //Destroy(gameObject);
    }
}
