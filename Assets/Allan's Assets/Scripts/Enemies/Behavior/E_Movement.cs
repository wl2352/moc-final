using UnityEngine;

public class E_Movement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followDistance = 5f;
    public float stopDistance = 1.5f;
    public Vector3 direction;
    private Stats stats;
    private void Start()
    {
        // Find the Stats script attached to the same GameObject
        stats = GetComponent<Stats>();
        // Find the player to follow
        player = FindObjectOfType<PlayerMovement>().transform;
    }
    private void Update()
    {
        if (player != null)
        {
            // Calculate distance between enemy and player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if player is within follow distance
            if (distanceToPlayer <= followDistance && distanceToPlayer > stopDistance)
            {
                // Move towards the player
                direction = (player.position - transform.position).normalized;
                transform.Translate(direction * stats.Speed * Time.deltaTime);
                //transform.position += direction * stats.Speed * Time.deltaTime;
            }

            else if (distanceToPlayer <= stopDistance || distanceToPlayer > followDistance)
            {
                direction = Vector3.zero;
            }
        }
        
    }
}
