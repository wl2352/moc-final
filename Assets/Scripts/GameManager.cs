using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool devMode = false;
    TextMeshProUGUI textMeshProUGUI;
    [Header("Enemies")]
    [SerializeField] List<E_Stats> enemies = new List<E_Stats>();
    [SerializeField] List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    [Space(5f)]
    [Header("Wave Configs")]
    public bool levelPassed = false;
    public int enemiesToKillGoal = 0;
    public int enemiesKilled = 0;
    public int maxWaves;
    public int currWave = 0;
    public int spawnerMaxEnemiesIncrement;
    public int spawnerSpawnTimeFactor;

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
        enemies = FindObjectsOfType<E_Stats>().ToList();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        SetWaveEnemyStats();
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"{enemiesToKillGoal} at Update() in gameobject: {gameObject}");
        enemies = FindObjectsOfType<E_Stats>().ToList();

        if (levelPassed)
        {
            Debug.Log("Level has been passed");
        }

        if (enemiesKilled < enemiesToKillGoal)
        {
            SpawnEnemies();
        }
        else
        {
            if (currWave >= maxWaves)
            {
                levelPassed = true;
            }
            else
            {
                IncrementWave();
            }
            //Debug.Log($"Reached\t Enemies killed: {enemiesKilled}\t Goal: {enemiesToKillGoal}");
        }

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
            //Debug.Log(spawner.MaxEnemies);
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
        if (gameObject.name == "Enemies Left")
        {
            textMeshProUGUI.text = enemies.Count.ToString();
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
        if (currWave < maxWaves)
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
            }

            // Reset enemy killed and get new enemy kill goal
            SetWaveEnemyStats();
        }
    }
}
