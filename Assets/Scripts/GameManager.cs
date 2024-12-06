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
    public float DestroyPositionY = -5.0f;
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

        if (enemiesList.Count > 0)
        {
            for (int i = 0; i < enemiesList.Count; i++)
            {
                GameObject enemy = enemiesList[i];
                if (enemy == null)
                {
                    enemiesList.RemoveAt(i);
                    Debug.Log("Removed destroyed enemy.");
                    i--;
                }
                else if (enemy.transform.position.y < DestroyPositionY)
                {
                    enemiesList.RemoveAt(i);
                    enemy.GetComponent<Enemy>().CleanUpAndDestroy();
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
