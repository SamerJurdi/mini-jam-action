using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D myRBody;
    public float mySpeed = 6.0f;
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
        if (GameManager.GM.GetTimePassed() % ShotInterval < Time.deltaTime)
        {
            // fire a shot
            Instantiate(LaserPrefab, new Vector3(this.myRBody.transform.position.x, this.myRBody.transform.position.y + ShotSpawningDistanceY, 0.0f), Quaternion.identity);
        }
    }

    public void CleanUpAndDestroy()
    {
        Debug.Log("Destroying Enemy");
        //Destroy(gameObject);
    }
}
