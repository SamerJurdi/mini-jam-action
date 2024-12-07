using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTimePassed : MonoBehaviour
{
    public float lifetime = 10f;
    float initTime;
    // Start is called before the first frame update
    void Start()
    {
        initTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (initTime + lifetime < Time.time)
            Destroy(gameObject);
    }
}
