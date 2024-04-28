using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Attack : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    private Stats stats;
    private E_Movement movement;

    [SerializeField] private GameObject bulletCast;
    public float attackCooldown = 2f; // Cooldown between attacks
    public bool canAttack = true; // Flag to control attack cooldown

    void Start()
    {
        stats = GetComponent<Stats>();
        movement = GetComponent<E_Movement>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        // Calculate distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within attack range and attack cooldown is over
        if (distanceToPlayer <= movement.followDistance && canAttack)
        {
            AttackPlayer();
        }
    }
    private void AttackPlayer()
    {
        GameObject bullet = Instantiate(bulletCast, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
        float speed = bullet.GetComponent<Bullet>().speed;
        bullet.GetComponent<Bullet>().damage = stats.Attack;
        bullet.GetComponent<Rigidbody2D>().AddForce(movement.direction * speed, ForceMode2D.Impulse);

        // Set attack cooldown
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
