using UnityEngine;

public class E_Attack : MonoBehaviour
{
    public float attackRange = 1.0f; // Range within which the enemy can attack
    public float attackCooldown = 2f; // Cooldown between attacks
    private Transform player; // Reference to the player's transform
    private Stats stats;
    public bool canAttack = true; // Flag to control attack cooldown

    private void Start()
    {
        // Find the Stats script attached to the same GameObject
        stats = GetComponent<Stats>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
    }

    private void Update()
    {
        if (player.GetComponent<Stats>().currentHP <= 0){
            return;
        }

        // Calculate distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within attack range and attack cooldown is over
        if (distanceToPlayer <= attackRange && canAttack)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Deal damage to the player
        player.GetComponent<Stats>().TakeDamage(stats.Attack);

        // Set attack cooldown
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
