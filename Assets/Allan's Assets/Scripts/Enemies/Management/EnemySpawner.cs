using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    GameManager gameManager;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    public float offset;
    private int SpawnedEnemies = 0;
    public int MaxEnemies = 5;
    public float SpawnTime;
    [SerializeField] private float timer;

    public void Spawn()
    {
        timer += Time.deltaTime;
        if (timer >= SpawnTime) timer = 0f;
        if (SpawnedEnemies >= MaxEnemies) { CancelInvoke(); return; }
        InvokeRepeating("SpawnEnemies", 0f, SpawnTime);
    }

    void SpawnEnemies()
    {
        if (SpawnedEnemies < MaxEnemies)
        {
            float randomSpawnOffset = Random.Range(1f, offset);
            
            if (enemies.Count != 0)
            {
                GameObject enemyPrefab = enemies[Random.Range(0, enemies.Count)];
                Vector3 spawnPosition = new Vector3(transform.position.x + randomSpawnOffset, transform.position.y + randomSpawnOffset, 0f);

                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                SpawnedEnemies++;
            }
            else
            {
                Debug.LogError("No enemies in the spawner. In the inspector, add an enemy prefab to the spawner object.");
                return;
            }
            
        }

    }

    public void ReInitialize(int newMaxEnemies, float newSpawnTime)
    {
        SpawnedEnemies = 0;
        SpawnTime += newSpawnTime;
        MaxEnemies += newMaxEnemies;
        CancelInvoke();
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
}
