using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP;
    public float Attack = 10;
    public float Defense = 5;
    public float Speed = 5f;

    ////camera shake
    public CameraShake cameraShake;
    public float shakeDuration;
    public float shakeMagnitude;

    public float knockbackForce = 0.1f; // Magnitude of the knockback force
    public float knockbackDuration = 0.5f;
    private Vector2 previousDirection; // Store the previous movement direction

    //public TextMeshProUGUI damageTextPrefab; // Prefab of the damage text to display
    //public Transform textSpawnPoint; // Spawn point for the damage text

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
    }

    public void TakeDamage(float damage)
    {
        float damageCalc = damage - Defense;
        damageCalc = Mathf.Clamp(damageCalc, 1, int.MaxValue); // Ensure damage is at least 1
        currentHP -= damageCalc;

        // Print a message to the console
        Debug.Log($"{gameObject} was attacked for " + damageCalc + " damage!");

        Vector2 knockbackDirection = (transform.position).normalized;

        // Apply knockback
        //ApplyKnockback(knockbackDirection);

        // Shake the camera when the player takes damage
  
        //cameraShake.Shake(shakeDuration, shakeMagnitude);
        

        if (currentHP <= 0)
        {
            Die(); 
        }
    }

    //void ShowDamageText(float damage)
    //{
    //    // Instantiate damage text prefab at the spawn point
    //    TextMeshProUGUI damageText = Instantiate(damageTextPrefab, textSpawnPoint.position, Quaternion.identity);

    //    // Set the damage amount
    //    damageText.text = "-" + damage;

  
    //}

    private void ApplyKnockback(Vector2 knockbackDirection)
    {
        // Calculate the knockback vector
        Vector2 knockbackVector = knockbackDirection * knockbackForce;

        // Move the GameObject away from the point of impact
        transform.position += (Vector3)knockbackVector;

        // Reset position after knockback duration
        Invoke("ResetPosition", knockbackDuration);
    }

    private void ResetPosition()
    {
        // Reset position to original position or any desired location
        // For example, you can reset it to the previous position or a spawn point

        // Apply opposite force to move in the opposite direction
        Vector2 oppositeDirection = -previousDirection;
        ApplyKnockback(oppositeDirection);
    }


    private void Die()
    {    
        if (isEnemy)
        {
            Debug.Log($"{gameObject} has died!");
            float num = Random.Range(0, 10);
            if (num <= lootRate)
            {
                Instantiate(loot[Random.Range(0, loot.Count)], transform.position, Quaternion.identity);
                OnEnemyKilled?.Invoke(this);
            }
            return;
        }
        SceneManager.LoadScene("Game Over");
        Destroy(gameObject);
    }
}
