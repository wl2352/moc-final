using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP;
    public float Attack = 10;
    public float Defense = 5;
    public float Speed = 5f;

    private void Start()
    {
        currentHP = maxHP; // Initialize current HP to max HP when the game starts
    }

    // You can add methods to modify stats, such as taking damage, dealing damage, etc.

    public void TakeDamage(float damage)
    {
        float damageCalc = damage - Defense;
        damageCalc = Mathf.Clamp(damageCalc, 1, int.MaxValue); // Ensure damage is at least 1
        currentHP -= damageCalc;
        // Print a message to the console
        Debug.Log("Object was attacked for " + damageCalc + " damage!");

        if (currentHP <= 0)
        {
            Die(); // Implement this method to handle player death
        }
    }
    
    private void Die()
    {
        Debug.Log("Object has died!");

        // Only drop loot if the gameObject is an enemy
        if (CompareTag("Enemy")) {
            GetComponent<E_Drop>().DropLoot();
        }
        
        Destroy(gameObject);
    }
}
