using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [Header("Player Settings")]
    public GameObject playerPrefab;
    [Tooltip("Initial position at the bottom")]
    public Vector2 playerStartPos = new Vector2(0f, -10f);
    [Tooltip("Final position after ascending")]
    public Vector2 playerFinalPos = new Vector2(0f, -4f);
    [Tooltip("Speed of the player's ascent")]
    public float playerAscendSpeed = 2f;
    [Tooltip("Time to wait before respawning the player")]
    public float respawnCooldown = 3f;
    private GameObject playerInstance;
    private bool isPlayerAscending = false;

    [Header("Enemies")]
    public GameObject EnemyPrefab;
    public float maxIntervalBetweenSpawns = 5.0f;
    public float minIntervalBetweenSpawns = 2.0f;
    public float maxIntervalTime = 360.0f;
    public float SpawnPositionY = 0.0f;
    public float SpawnPositionMinX = -5.0f;
    public float SpawnPositionMaxX = 5.0f;
    public float DestroyPositionY = -5.0f;
    private List<GameObject> enemiesList = new List<GameObject>();
    private float timePassed;
    private float lastSpawnTime;

    [Header("GameStats")]
    public int maxEarthHealth = 5;
    private int earthHealth;
    public int Score = 0;
    public int ScoreIncreasePerSecond = 10;

    [Header("ObjectReferences")]
    public GameObject resetPanel;

    void Start()
    {
        if (GameManager.GM == null)
            GameManager.GM = this;
        
        SpawnPlayer();
        earthHealth = maxEarthHealth;
    }

    void Update()
    {
        HandlePlayerAscent();

        // every second, we get points
        if (Mathf.Floor(timePassed) < Mathf.Floor(timePassed + Time.deltaTime))
            GameManager.GM.AddScore(ScoreIncreasePerSecond);
        // instantiate enemies on a time interval in a random position
        timePassed += Time.deltaTime;

        // Do wave spawning
        float intervalBetweenSpawns = minIntervalBetweenSpawns;
        if (timePassed < maxIntervalTime) // we stop decreasing spawn time at the maximum length of time
            intervalBetweenSpawns += ((maxIntervalBetweenSpawns - minIntervalBetweenSpawns) * ((maxIntervalTime - timePassed) / maxIntervalTime));

        if (timePassed > lastSpawnTime + intervalBetweenSpawns)
        {
            enemiesList.Add(Instantiate(EnemyPrefab, new Vector3(Random.Range(SpawnPositionMinX, SpawnPositionMaxX), SpawnPositionY, 0), Quaternion.identity));
            lastSpawnTime += intervalBetweenSpawns;
        }

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

    public float GetEarthHealth()
    {
        return (float)earthHealth / (float)maxEarthHealth;
    }

    public void DamageEarth(int damageAmount)
    {
        // Handle Earth Health Logic
        Debug.Log(damageAmount);
        earthHealth -= damageAmount;
        if (earthHealth < 0)
            earthHealth = 0;
        if (earthHealth <= 0)
        {
            Debug.Log("Earth is dead"); // TODO: Trigger game over sequence
            Time.timeScale = 0;
            resetPanel.SetActive(true);
        }
    }

    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            // Spawn the player at the starting position
            playerInstance = Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
            isPlayerAscending = true;
        }
    }

    private void HandlePlayerAscent()
    {
        if (isPlayerAscending && playerInstance != null)
        {
            // Move the player towards the final position
            playerInstance.transform.position = Vector2.MoveTowards(
                playerInstance.transform.position,
                playerFinalPos,
                playerAscendSpeed * Time.deltaTime
            );

            // Stop moving when the player reaches the final position
            if (Vector2.Distance(playerInstance.transform.position, playerFinalPos) < 0.01f)
            {
                isPlayerAscending = false;
                Debug.Log("Player ascent complete.");
            }
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Player died. Respawning...");
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }
        StartCoroutine(RespawnPlayerAfterCooldown());
    }

    public void AddScore(int scoreIncrease)
    {
        Score += scoreIncrease;
    }

    private IEnumerator RespawnPlayerAfterCooldown()
    {
        yield return new WaitForSeconds(respawnCooldown);
        SpawnPlayer();
        Debug.Log("Player respawned.");
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}
