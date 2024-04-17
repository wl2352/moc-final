using UnityEngine;

public class P_Attack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackCooldown = 2f; // Cooldown between attacks
    public LayerMask enemyLayer;
    private Stats stats;
    private bool canAttack = true; // Flag to control attack cooldown
    
    private void Start()
    {
        // Find the Stats script attached to the same GameObject
        stats = GetComponent<Stats>();
    }
    private void Update()
    {
        if (stats.currentHP <= 0){
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (canAttack)
        {
            // Detect enemies within attack range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

            // Deal damage to each enemy
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Stats>().TakeDamage(stats.Attack);
            }

            // Set attack cooldown
            canAttack = false;
            Invoke("ResetAttackCooldown", attackCooldown);
        }
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
