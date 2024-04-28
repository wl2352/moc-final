using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] public float offset;
    P_ColorSwitch P_ColorSwitch;
    Animator animator;
    List<Color> availableColors = new List<Color>(){
        Color.red,
        Color.blue,
        Color.yellow
    };

    private void Start()
    {
        P_ColorSwitch = FindObjectOfType<P_ColorSwitch>();
        /*if (P_ColorSwitch != null)
        {
            if (P_ColorSwitch.redUnlocked)
            {
                availableColors.Add(Color.red);
            }
            if (P_ColorSwitch.yellowUnlocked)
            {
                availableColors.Add(Color.yellow);
            }
            if (P_ColorSwitch.blueUnlocked)
            {
                availableColors.Add(Color.blue);
            }
        }*/
    }

    public void Spawn()
    {
        float randomSpawnOffset = Random.Range(1f, offset);
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Count)];
        Color randomColor = availableColors[Random.Range(0, availableColors.Count)];
        enemyPrefab.GetComponent<E_Colors>().color = randomColor;
        enemyPrefab.AddComponent<Animator>(); // add each prefab its own Animator component

        animator = enemyPrefab.GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load("Assets/Art/Enemies/Animations/WolfAnimations/Wolf.controller"));
        Vector3 spawnPosition = new Vector3(transform.position.x + randomSpawnOffset, transform.position.y + randomSpawnOffset, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
}
