using UnityEngine;

public class E_Movement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followDistance = 5f;
    public float stopDistance = 1.5f;
    private Stats stats;
    private void Start()
    {
        // Find the Stats script attached to the same GameObject
        stats = GetComponent<Stats>();
    }
    private void Update()
    {
        // Calculate distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within follow distance
        if (distanceToPlayer <= followDistance && distanceToPlayer > stopDistance)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * stats.Speed * Time.deltaTime);
        }

        else if (distanceToPlayer <= stopDistance)
        {
            // Stop moving if close enough to the player
        }
    }
}
