using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    GameManager gameManager;
    [SerializeField] private GameObject enemyPrefab;
    public float offset = 0;
    private int SpawnedEnemies = 0;
    public int MaxEnemies = 5;
    public float SpawnTime = 0;

    public void Spawn()
    {
        if (SpawnedEnemies >= MaxEnemies) { CancelInvoke(); return; }
        InvokeRepeating("SpawnEnemies", 0f, SpawnTime);
    }

    void SpawnEnemies()
    {
        if (SpawnedEnemies < MaxEnemies)
        {
            float SpawnPosition = UnityEngine.Random.Range(1f, offset);

            UnityEngine.Vector3 SpawnerPosition = new UnityEngine.Vector3(transform.position.x + SpawnPosition, transform.position.y + SpawnPosition, 0f);

            Instantiate(enemyPrefab, SpawnerPosition, Quaternion.identity);
            SpawnedEnemies++;
        }

    }

    public void ReInitialize(int newMaxEnemies, float newSpawnTime)
    {
        SpawnedEnemies = 0;
        SpawnTime += newSpawnTime;
        MaxEnemies += newMaxEnemies;
        CancelInvoke();
    }
}
