using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static GameManager GM;
    public string nameOfSomethihg;
    public GameObject Player;
    [Header("Enemies")]
    public GameObject EnemyPrefab;
    public float intervalBetweenSpawns = 5.0f;
    public float SpawnPositionY = 0.0f;
    public float SpawnPositionMinX = -5.0f;
    public float SpawnPositionMaxX = 5.0f;
    public float DestroyPositionY = -100.0f;
    private List<GameObject> enemiesList = new List<GameObject>();
    private float timePassed;
    public string thisIsATest;
    public string JustTesting;
    public int thisIsMyNumber;
    // Start is called before the first frame update
    void Start()
    {
        //

        //if (GameManager.GM == null)
            //GameManager.GM = this;
        //
    }

    // Update is called once per frame
    void Update()
    {
        // instantiate enemies on a time interval in a random position
        timePassed += Time.deltaTime;
        if (timePassed % intervalBetweenSpawns <= Time.deltaTime)
            enemiesList.Add(Instantiate(EnemyPrefab, new Vector3(Random.Range(SpawnPositionMinX,SpawnPositionMaxX),SpawnPositionY,0),Quaternion.identity));
        if (enemiesList.Capacity > 0)
        {
            for (int i = 0; i < enemiesList.Count; i++)
            {
                if (enemiesList[i].transform.position.y < DestroyPositionY)
                {
                    GameObject temp = enemiesList[i];
                    enemiesList.Remove(temp);
                    Destroy(temp);
                    i--;
                }
            }
        }
    }

    public float GetTimePassed()
    {
        return timePassed;
    }
}
