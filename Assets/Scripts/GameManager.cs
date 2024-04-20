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
    PlayerStats playerStats;
    [Header("Enemies")]
    [SerializeField] List<E_Stats> enemies = new List<E_Stats>();
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    [Space(5f)]
    [Header("Wave Configs")]
    [SerializeField] private bool levelPassed = false;
    [SerializeField] private int enemiesToKillGoal = 0;
    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private int maxWaves;
    [SerializeField] private int currWave = 1;
    [SerializeField] private int hazardWave;
    [SerializeField] private int newEnemiesWave;
    [SerializeField] private int spawnerMaxEnemiesIncrement;
    [SerializeField] private int spawnerSpawnTimeFactor;

    [Space(5f)]
    [Header("Map Control")]
    [SerializeField] private GameObject[] barriers;
    [SerializeField] private GameObject hazards;
    [SerializeField] private GameObject newEnemy;
    [SerializeField] private GameObject levelClearedBarrier;


    private void OnEnable()
    {
        E_Stats.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        E_Stats.OnEnemyKilled -= HandleEnemyDefeated;
    }

    void Awake()
    {
        // Entities
        playerStats = FindObjectOfType<PlayerStats>();
        enemies = FindObjectsOfType<E_Stats>().ToList();

        // Map Objects
        levelClearedBarrier = GameObject.FindGameObjectWithTag("Finish");
        barriers = GameObject.FindGameObjectsWithTag("Barrier");

        // Initialize first wave
        SetWaveEnemyStats();

        // Error Handling
        if (hazardWave > maxWaves)
        {
            Debug.LogError($"Hazard wave value, {hazardWave} cannot be greater than max waves, {maxWaves}");
        }
        if (newEnemiesWave > maxWaves)
        {
            Debug.LogError($"Enemies wave value, {newEnemiesWave} cannot be greater than max waves, {maxWaves}");
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Track current amount of enemies
        enemies = FindObjectsOfType<E_Stats>().ToList();

        if (levelPassed)
        {
            LevelCleared();
        }

        // Logic: If the current wave is less than or at the last wave...
        if (currWave <= maxWaves)
        {
            // ...and the player hasn't completed the wave, keep spawning enemies
            if (enemiesKilled < enemiesToKillGoal)
            {
                SpawnEnemies();
            }
            // ...and the player has completed the wave, start the next wave
            else
            {
                IncrementWave();
            }
        }
        // If the player completed all waves, they passed the level
        else
        {
            levelPassed = true;
        }

        // Game controls (may vary per scene)
        if (SceneManager.GetActiveScene().name != "Overworld")
        {
            GameControls();

        }

        else 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("Demo");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene("Scene1");
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

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
            SceneManager.LoadScene("Overworld");
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
        if (!newEnemy.TryGetComponent(out E_Stats enemy)) return;

        spawner.AddEnemy(newEnemy);
    }

    void HandleEnemyDefeated(E_Stats enemy)
    {
        enemiesKilled += 1;
        enemies.Remove(enemy);
    }

    void SetWaveEnemyStats()
    {
        enemiesKilled = 0;
        enemiesToKillGoal = 0;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            enemiesToKillGoal += spawner.MaxEnemies;
            Debug.Log(enemiesToKillGoal);
        }
    }

    void GameControls()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Overworld");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // Currently bugged and crashing game for some reason
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            devMode = !devMode;
        }
        if (FindObjectsOfType<PlayerStats>().ToList().Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void SpawnEnemies()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.Spawn();
        }
    }

    void IncrementWave()
    {
        if (currWave <= maxWaves)
        {
            // Increment current wave
            currWave++;

            // Destroy existing enemies
            List<E_Stats> remainingEnemies = FindObjectsOfType<E_Stats>().ToList();
            if (remainingEnemies.Count > 0)
            {
                foreach(E_Stats e in remainingEnemies)
                {
                    Destroy(e.gameObject);
                }
            }

            // Increase the maximum amount each spawner can spawn an enemy by the desired enemy increase value
            // Also, refresh each enemy spawner with their new values
            foreach (EnemySpawner spawner in enemySpawners)
            {
                spawner.ReInitialize(spawnerMaxEnemiesIncrement, spawnerSpawnTimeFactor);

                // Check if the new wave is the new enemies wave, if so, add the new enemy to the spawners
                if (currWave == newEnemiesWave)
                {
                    AddEnemyToSpawner(spawner);
                }
            }

            // Check if the new wave is the hazard wave, if so, activate the hazard
            if (currWave == hazardWave)
            {
                ActivateGroundHazards();
            }

            // Reset enemy killed and get new enemy kill goal
            SetWaveEnemyStats();
        }
    }
}
