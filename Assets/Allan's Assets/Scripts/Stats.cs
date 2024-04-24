using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP = 0;
    public float Attack = 10;
    public float Defense = 5;
    public float Speed = 5f;

    [Header("Player Specific")]
    public bool isPlayer;
    public int currency = 0;

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
            if (currency == 0)
            {
                PlayerPrefs.SetInt("Currency", currency);
            }

            // If player has died or just started, instantiate all stats to default
            if (currentHP <= 0)
            {
                currentHP = maxHP;
                currency = 0;
                PlayerPrefs.SetInt("Currency", currency);
                PlayerPrefs.SetFloat("CurrentHP", currentHP);

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

    // You can add methods to modify stats, such as taking damage, dealing damage, etc.

    public void TakeDamage(float damage)
    {
        float damageCalc = damage - Defense;
        damageCalc = Mathf.Clamp(damageCalc, 1, int.MaxValue); // Ensure damage is at least 1
        currentHP -= damageCalc;
        PlayerPrefs.SetFloat("CurrentHP", currentHP);
        // Print a message to the console
        Debug.Log($"{gameObject} was attacked for " + damageCalc + " damage!");

        if (currentHP <= 0)
        {
            Die(); // Implement this method to handle player death
        }
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
}
