using UnityEngine;

public class P_Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the character
    public float attackCooldown = 1f; // Cooldown time between attacks

    private Rigidbody2D rb;
    private bool canAttack = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);

        

        // Attack input
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Attack();
        }

        // Calculate movement velocity
        Vector2 movementVelocity = movementDirection * moveSpeed;
        rb.velocity = movementVelocity;
        FindObjectOfType<P_Animation>().SetDirection(movementDirection);
    }

    private void Attack()
    {
        // Perform attack logic here
        Debug.Log("Attacking!");

        // Disable attack temporarily to prevent rapid attacks
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
