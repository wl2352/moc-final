using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Attack : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 2f;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask playerLayer;

    public bool canAttack = true;
    private Animator animator;
    private Rigidbody2D enemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack) 
        {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            foreach (Collider2D player in hitPlayer)
            {
                Attack(player.gameObject);
                Cooldown();
            }
        }
    }

    void Attack(GameObject player)
    {
        if (GetComponent<E_Movement>().E_direction.y > 0) {
            animator.SetBool("AttackingUp", true);
        }
        else {
            animator.SetBool("Attacking", true);
        }

        player.GetComponent<PlayerStats>().TakeDamage(gameObject.GetComponent<E_Stats>().damage);
        Debug.Log("Attacked");
    }

    void Cooldown()
    {
        canAttack = false;
        Invoke("ResetAttack", attackCooldown);
    } 

    void ResetAttack()
    {
        canAttack = true;
        animator.SetBool("AttackingUp", false);
        animator.SetBool("Attacking", false);
    }

    void gizmo()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
