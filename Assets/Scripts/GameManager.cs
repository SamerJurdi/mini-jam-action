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
    private PlayerController playerController;
    private bool isPlayerAscending = false;

    [Header("Enemies")]
    public List<GameObject> EnemyPrefabs;
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
    public AudioClip gameMusicRef;
    public AudioClip deathMusicRef;
    public GameObject playOnAwakeRef;

    void Start()
    {
        if (GameManager.GM == null)
            GameManager.GM = this;
        
        SpawnPlayer();
        earthHealth = maxEarthHealth;
        MusicManager.MM.PlayMusic(gameMusicRef, 0.75f, true);
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
        if (timePassed < maxIntervalTime)
            intervalBetweenSpawns += ((maxIntervalBetweenSpawns - minIntervalBetweenSpawns) * ((maxIntervalTime - timePassed) / maxIntervalTime));
        // check if it's time to spawn an enemy ship
        if (timePassed > lastSpawnTime + intervalBetweenSpawns)
        {
            // Spawn enemy ship
            int enemyShipType = 0;
            if (timePassed < 20)
                enemyShipType = 0;
            else if (timePassed < 40)
                enemyShipType = 1;
            else if (timePassed < 50)
            {
                if (Random.Range(0,100f) < 75f)
                    enemyShipType = 0;
                else
                    enemyShipType = 1;
                intervalBetweenSpawns = 5f;
            }
            else if (timePassed < 80)
            {
                if (Random.Range(0, 100f) < 80f)
                    enemyShipType = 0;
                else
                    enemyShipType = 1;
                intervalBetweenSpawns = 1.5f;
            }
            else if (timePassed < 90)
            {
                if (Random.Range(0, 100f) < 80f)
                    enemyShipType = 0;
                else
                    enemyShipType = 1;
                intervalBetweenSpawns = 5f;
            }
            else if (timePassed < 120)
            {
                    enemyShipType = 0;
                intervalBetweenSpawns = 1.2f;
            }
            else if (timePassed < 140)
            {
                    enemyShipType = 0;
                intervalBetweenSpawns = 2.5f;
            }
            else if (timePassed < 160)
            {
                if (Random.Range(0, 100f) < 75f)
                    enemyShipType = 0;
                else
                    enemyShipType = 1;
                intervalBetweenSpawns = 2.5f;
            }
            else if (timePassed < 180)
            {
                if (Random.Range(0, 100f) < 80f)
                    enemyShipType = 0;
                else
                    enemyShipType = 1;
                intervalBetweenSpawns = 2.2f;
            }
            else if (timePassed < 200)
            {
                enemyShipType = 1;
                intervalBetweenSpawns = 2.8f;
            }
            else if (timePassed < 210)
            {
                if (Random.Range(0, 100f) < 75f)
                    enemyShipType = 0;
                else
                    enemyShipType = 1;
                intervalBetweenSpawns = 4.5f;
            }
            else if (timePassed < 240)
            {
                enemyShipType = 0;
                intervalBetweenSpawns = 2.3f;
            }
            else
                enemyShipType = Mathf.FloorToInt(Random.Range(0, 1.99f));
            
            SpawnEnemy(EnemyPrefabs[enemyShipType]);
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
                    enemyController.DestroyWithEffect(false);
                    i--;
                }
            }
        }

    }

    public void SpawnEnemy(GameObject enemyRef)
    {
        enemiesList.Add(Instantiate(enemyRef, new Vector3(Random.Range(SpawnPositionMinX, SpawnPositionMaxX), SpawnPositionY, 0), Quaternion.identity));
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
            MusicManager.MM.PlayMusic(deathMusicRef, 1f, false);
            Debug.Log("Earth is dead"); // TODO: Trigger game over sequence
            Time.timeScale = 0;
            resetPanel.SetActive(true);
        }
    }

    public void HealEarth(int healAmount)
    {
        // Handle Earth Health Logic
        earthHealth += healAmount;
        if (earthHealth > maxEarthHealth)
            earthHealth = maxEarthHealth;
    }

    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            // Spawn the player at the starting position
            playerInstance = Instantiate(playerPrefab, playerStartPos, Quaternion.identity);
            playerController = playerInstance.GetComponent<PlayerController>();
            playerController.isPlayerActive = false;
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
                playerController.isPlayerActive = true;
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

    public void PlaySound(AudioClip clipToPlay)
    {
        GameObject temp = Instantiate(playOnAwakeRef, transform.position, Quaternion.identity);
        AudioSource tempAudioSource = temp.GetComponent<AudioSource>();
        tempAudioSource.clip = clipToPlay;
        tempAudioSource.Play();
    }
}
