using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string nameOfSomethihg;
    public GameObject Player;
    [Header("Enemies")]
    public GameObject EnemyPrefab;
    public int intervalBetweenSpawns;
    private float timePassed;
    public string thisIsATest;
    public string JustTesting;
    public int thisIsMyNumber;
    // Start is called before the first frame update
    void Start()
    {
        //

        //
    }

    // Update is called once per frame
    void Update()
    {
        // instantiate enemies on a time interval in a random position
        Instantiate(EnemyPrefab);
    }
}
