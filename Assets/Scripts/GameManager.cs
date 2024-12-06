using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static GameManager GM;

    [Header("Enemies")]
    public GameObject EnemyPrefab;
    public float intervalBetweenSpawns = 5.0f;
    public float SpawnPositionY = 0.0f;
    public float SpawnPositionMinX = -5.0f;
    public float SpawnPositionMaxX = 5.0f;
    public float DestroyPositionY = -5.0f;
    private List<GameObject> enemiesList = new List<GameObject>();
    private float timePassed;

    void Start()
    {
        //if (GameManager.GM == null)
            //GameManager.GM = this;
        //
    }

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
                    // Enemy Collided with Earth
                    enemiesList.RemoveAt(i);
                    Enemy enemyController = enemy.GetComponent<Enemy>();
                    DamageEarth(enemyController.earthDamage);
                    enemyController.CleanUpAndDestroy();
                    i--;
                }
            }
        }

    }

    public float GetTimePassed()
    {
        return timePassed;
    }

    public void DamageEarth(int damageAmount)
    {
        // Handle Earth Health Logic
        Debug.Log(damageAmount);
    }
}
