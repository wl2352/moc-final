using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP;
    public float Attack = 10;
    public float Defense = 5;
    public float Speed = 5f;

    [Header("Enemy Specific")]
    public bool isEnemy = false;
    [SerializeField]
    List<GameObject> loot = new List<GameObject>();
    [SerializeField]
    float lootRate = 2f;

    public static event System.Action<Stats> OnEnemyKilled;

    private void Start()
    {
        currentHP = maxHP; // Initialize current HP to max HP when the game starts

        
        switch (gameObject.tag)
        {
            case "Player":
                Attack = 10;
                Defense = 5;
                Speed = 5f;
                isEnemy = false;
                break;
            case "Wolf":
                Attack = 5.0f;
                Defense = 4.0f;
                Speed = 1.0f;
                isEnemy = true;
                break;
            case "Flyer":
                Attack = 1.0f;
                Defense = 3.0f;
                Speed = 1.5f;
                isEnemy = true;
                break;
            default:
                return;
        }
        
    }

    // You can add methods to modify stats, such as taking damage, dealing damage, etc.

    public void TakeDamage(float damage)
    {
        float damageCalc = damage - Defense;
        damageCalc = Mathf.Clamp(damageCalc, 1, int.MaxValue); // Ensure damage is at least 1
        currentHP -= damageCalc;
        // Print a message to the console
        Debug.Log($"{gameObject} was attacked for " + damageCalc + " damage!");

        if (currentHP <= 0)
        {
            Die(); // Implement this method to handle player death
        }
    }

    private void Die()
    {
        // Implement your death logic here, such as respawning or game over
        
        Debug.Log($"{gameObject} has died!");

        if (isEnemy)
        {
            float num = Random.Range(0, 10);
            if (num <= lootRate)
            {
                Instantiate(loot[Random.Range(0, loot.Count)], transform.position, Quaternion.identity);
                OnEnemyKilled?.Invoke(this);
            }
        }

        Destroy(gameObject);
    }
}
