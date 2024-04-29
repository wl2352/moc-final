using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP = 0;
    public float Attack = 10;
    public float Defense = 5;
    public float Speed = 5f;
    public GameObject damageNumberPrefab;

    [Header("Player Specific")]
    public bool isPlayer;
    public int currency = 0;
    public int levelsCleared = 0;

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
    [SerializeField] float gemRate = 2f;
    [SerializeField] float currencyRate = 9f;
    [SerializeField] GameObject currencyObj;

    private void Start()
    {
        if (isPlayer)
        {
            PlayerPrefs.SetFloat("Attack", Attack);
            PlayerPrefs.SetFloat("Defense", Defense);
            PlayerPrefs.SetFloat("Speed", Speed);

            currency = PlayerPrefs.GetInt("Currency");
            currentHP = PlayerPrefs.GetFloat("CurrentHP");
            levelsCleared = PlayerPrefs.GetInt("LevelsCleared");
            /*if (currency == 0)
            {
                PlayerPrefs.SetInt("Currency", currency);
            }*/
            if (levelsCleared == 0)
            {
                PlayerPrefs.SetInt("LevelsCleared", levelsCleared);
            }

            // If player has died or just started, instantiate all stats to default
            if (currentHP <= 0)
            {
                currentHP = maxHP;
                // currency = 0;
                levelsCleared = 0;
                // PlayerPrefs.SetInt("Currency", currency);
                currency = PlayerPrefs.GetInt("Currency");
                PlayerPrefs.SetFloat("CurrentHP", currentHP);
                PlayerPrefs.SetInt("LevelsCleared", levelsCleared);

                PlayerPrefs.SetInt("RedLevel", 0);
                PlayerPrefs.SetInt("YellowLevel", 0);
                PlayerPrefs.SetInt("BlueLevel", 0);
                PlayerPrefs.SetFloat("ColorCooldown", gameObject.TryGetComponent<P_ColorSwitch>(out P_ColorSwitch pc) ? pc.colorCooldown : 5f);
            }
        }
        else
        {
            currentHP = maxHP;
        }
        
    }

    public void TakeDamage(float damage)
    {
        float damageCalc = damage - Defense;
        damageCalc = Mathf.Clamp(damageCalc, 1, int.MaxValue); // Ensure damage is at least 1

        if (damageNumberPrefab != null)
        {
            GameObject damageNumber = Instantiate(damageNumberPrefab, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
            damageNumber.GetComponent<TextMesh>().text = damageCalc.ToString();
            if (isEnemy)
            {
                damageNumber.GetComponent<TextMesh>().characterSize = 0.5f;
                damageNumber.GetComponent<TextMesh>().color = Color.white;
            }
            else
            {
                damageNumber.GetComponent<TextMesh>().color = Color.red;
            }
            
        }
        
        currentHP -= damageCalc;

        if (isPlayer)
        {
            PlayerPrefs.SetFloat("CurrentHP", currentHP);
            gameObject.GetComponentInChildren<CameraShake>().ShakeCamera();
        }
        
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
            if (0 <= num && num <= gemRate)
            {
                Instantiate(loot[Random.Range(0, loot.Count)], transform.position, Quaternion.identity);
            }
            else if (gemRate < num && num < currencyRate)
            {
                Instantiate(currencyObj, transform.position, Quaternion.identity);
            }
            return;
        }
        SceneManager.LoadScene("Game Over");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPlayer)
        {
            if (collision.gameObject.CompareTag("Currency"))
            {
                currency++;
                PlayerPrefs.SetInt("Currency", currency);
                Destroy(collision.gameObject);
            }
        }
    }

    public bool OnPurchase(int cost)
    {
        if (currency >= cost)
        {
            currency -= cost;
            PlayerPrefs.SetInt("Currency", currency);
            return true;
        }
        return false;
    }

    public void IncreaseHealth(float hp)
    {
        currentHP += hp;
        if (currentHP >= maxHP) currentHP = maxHP;
        PlayerPrefs.SetFloat("CurrentHP", currentHP);
    }
}
