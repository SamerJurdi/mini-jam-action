using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Rigidbody2D myRBody;
    public float mySpeed = 6.0f;
    // Start is called before the first frame update
    void Start()
    {
        myRBody = GetComponent<Rigidbody2D>();
        myRBody.velocity = Vector2.down * mySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}