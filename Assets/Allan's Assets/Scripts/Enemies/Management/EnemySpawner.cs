using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] public float offset;

    public void Spawn()
    {
        float randomSpawnOffset = Random.Range(1f, offset);
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Count)];
        Vector3 spawnPosition = new Vector3(transform.position.x + randomSpawnOffset, transform.position.y + randomSpawnOffset, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
}
