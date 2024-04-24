using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool devMode = false;
    Stats playerStats;
    [Header("Enemies")]
    [SerializeField] List<EnemyAlive> enemies = new List<EnemyAlive>();
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    [Space(5f)]
    [Header("Wave Configs")]
    [SerializeField] private bool levelPassed = false;
    [SerializeField] private int enemiesPerWave = 0;    // Number of enemies per wave
    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private int totalWaves = 3;    // Total number of waves
    [SerializeField] private int currentWave = 0;   // Current wave number
    [SerializeField] private int hazardWave;
    [SerializeField] private int newEnemiesWave;
    //[SerializeField] private int spawnerMaxEnemiesIncrement;
    //[SerializeField] private int spawnerSpawnTimeFactor;
    [SerializeField] private int enemiesSpawned = 0; // Number of enemies spawned in the current wave
    [SerializeField] private float timer = 0f; // Timer to track spawn intervals
    [SerializeField] private bool spawningEnabled = true; // Flag to enable/disable spawning
    [SerializeField] public float waveInterval = 10f; // Time interval between waves
    [SerializeField] public float spawnInterval = 2f; // Time interval between enemy spawns

    [Space(5f)]
    [Header("Map Control")]
    [SerializeField] private GameObject[] barriers;
    [SerializeField] private GameObject hazards;
    [SerializeField] private GameObject newEnemy;
    [SerializeField] private GameObject levelClearedBarrier;
    [SerializeField] private GameObject shop;
    bool shopIsActive = false;

    [Space(5f)]
    [Header("Currency")]
    public int timeoutCost;
    public int redCost;
    public int yellowCost;
    public int blueCost;
    public int healthCost;


    private void OnEnable()
    {
        EnemyAlive.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        EnemyAlive.OnEnemyKilled -= HandleEnemyDefeated;
    }

    void Awake()
    {
        // Entities
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        enemies = FindObjectsOfType<EnemyAlive>().ToList();

        // Map Objects
        levelClearedBarrier = GameObject.FindGameObjectWithTag("Finish");
        barriers = GameObject.FindGameObjectsWithTag("Barrier");
        // shop = GameObject.FindGameObjectWithTag("ShopPanel");

        /*// Initialize first wave
        SetWaveEnemyStats();*/

        // Error Handling
        if (hazardWave > totalWaves)
        {
            Debug.LogError($"Hazard wave value, {hazardWave} cannot be greater than max waves, {totalWaves}");
        }
        if (newEnemiesWave > totalWaves)
        {
            Debug.LogError($"Enemies wave value, {newEnemiesWave} cannot be greater than max waves, {totalWaves}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Track current amount of enemies
        enemies = FindObjectsOfType<EnemyAlive>().ToList();

        if (levelPassed)
        {
            TrackLevelsCleared();
            LevelCleared();
        }
        else
        {
            if (spawningEnabled)
            {
                timer += Time.deltaTime;

                // Check if it's time to spawn an enemy
                if (timer >= spawnInterval && enemiesSpawned < enemiesPerWave)
                {
                    SpawnEnemy();
                    timer = 0f;
                    enemiesSpawned++;
                }

                // Check if all enemies in the current wave have been spawned
                if (enemiesKilled >= (enemiesPerWave * enemySpawners.Count))
                {
                    spawningEnabled = false;
                    Invoke("StartNextWave", waveInterval); // Start next wave after a delay
                }
            }
        }

        // Game controls (may vary per scene)
        if (SceneManager.GetActiveScene().name != "Overworld [Updated]")
        {
            GameControls();
        }

        else 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("Level 0 [Updated]");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && PlayerPrefs.GetInt("LevelsCleared") >= 1)
            {
                SceneManager.LoadScene("Level 1 [Updated]");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && PlayerPrefs.GetInt("LevelsCleared") >= 2)
            {
                SceneManager.LoadScene("Level 2 [Updated]");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && PlayerPrefs.GetInt("LevelsCleared") >= 3)
            {
                SceneManager.LoadScene("Level 3 [Updated]");
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && PlayerPrefs.GetInt("LevelsCleared") >= 4)
            {
                SceneManager.LoadScene("Will-Scene1");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    void SpawnEnemy()
    {
        // Instantiate enemy prefab at a random position
        foreach (EnemySpawner spawner in enemySpawners)
        {
            // spawner.Spawn();
            spawner.Spawn();
        }

    }

    void StartNextWave()
    {
        currentWave++;
        enemiesKilled = 0;
        // Destroy existing enemies
        List<EnemyAlive> remainingEnemies = FindObjectsOfType<EnemyAlive>().ToList();
        if (remainingEnemies.Count > 0)
        {
            foreach (EnemyAlive e in remainingEnemies)
            {
                Destroy(e.gameObject);
            }
        }
        if (currentWave <= totalWaves)
        {
            enemiesSpawned = 0;
            spawningEnabled = true;
            if (currentWave == newEnemiesWave)
            {
                foreach (EnemySpawner spawner in enemySpawners)
                {
                    AddEnemyToSpawner(spawner);
                }
            }

            if (currentWave == hazardWave)
            {
                ActivateGroundHazards();
            }
        }
        else
        {
            levelPassed = true;
            Debug.Log("All waves completed!");
        }

        Debug.Log($"PlayerPrefs Cur: {PlayerPrefs.GetInt("Currency")}, LevelsCleared: {PlayerPrefs.GetInt("LevelsCleared")}, HP: {PlayerPrefs.GetFloat("CurrentHP")}");
    }

    // Function to check if two GameObjects are colliding
    bool AreObjectsColliding(GameObject obj1, GameObject obj2)
    {
        Collider2D collider1 = obj1.GetComponent<Collider2D>();
        Collider2D collider2 = obj2.GetComponent<Collider2D>();

        if (collider1 != null && collider2 != null)
        {
            return collider1.IsTouching(collider2);
        }

        return false;
    }

    void LevelCleared()
    {
        if (barriers.Length > 0)
        {
            foreach (GameObject barrier in barriers)
            {
                barrier.SetActive(false);
            }
        }

        if (levelClearedBarrier != null && levelClearedBarrier.CompareTag("Finish") && AreObjectsColliding(playerStats.gameObject, levelClearedBarrier))
        {
            SceneManager.LoadScene("Overworld [Updated]");
        }
    }

    private void ActivateGroundHazards()
    {
        if (hazards == null) return;
        if (hazards.activeSelf) return;
        
        hazards.SetActive(true);
    }

    private void AddEnemyToSpawner(EnemySpawner spawner)
    {
        if (newEnemy == null) return;
        if (!newEnemy.TryGetComponent(out EnemyAlive enemy)) return;

        spawner.AddEnemy(newEnemy);
    }

    void HandleEnemyDefeated(EnemyAlive enemy)
    {
        enemiesKilled += 1;
        enemies.Remove(enemy);
    }

    void GameControls()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Overworld [Updated]");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (shop != null)
            {
                

                shopIsActive = !shopIsActive;
                shop.SetActive(shopIsActive);
                // pause time 
                Time.timeScale = shopIsActive ? 0 : 1;
                AudioListener.pause = shopIsActive;
            }
        }
        // Currently bugged and crashing game for some reason
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            devMode = !devMode;
        }
        if (playerStats == null)
        {
            SceneManager.LoadScene(8);
        }
    }

    void TrackLevelsCleared()
    {
        if (levelPassed && SceneManager.GetActiveScene().buildIndex == 3)
        {
            playerStats.levelsCleared = 1;
        }
        if (levelPassed && SceneManager.GetActiveScene().buildIndex == 4)
        {
            playerStats.levelsCleared = 2;
        }
        if (levelPassed && SceneManager.GetActiveScene().buildIndex == 5)
        {
            playerStats.levelsCleared = 3;
        }
        if (levelPassed && SceneManager.GetActiveScene().buildIndex == 6)
        {
            playerStats.levelsCleared = 4;
        }
        if (levelPassed && SceneManager.GetActiveScene().buildIndex == 7)
        {
            playerStats.levelsCleared = 5;
        }

        PlayerPrefs.SetInt("LevelsCleared", playerStats.levelsCleared);
    }
}
