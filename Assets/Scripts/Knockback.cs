using UnityEngine;

public class KnockbackOnHit : MonoBehaviour
{
    public float knockbackForce = 5f; // Magnitude of the knockback force
    public float knockbackDuration = 0.5f; // Duration of the knockback effect
    public LayerMask hitLayers; // Layers to check for collisions

    private Vector2 previousDirection; // Store the previous movement direction

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hitLayers == (hitLayers | (1 << other.gameObject.layer)))
        {
            // Calculate knockback direction
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;

            // Apply knockback
            ApplyKnockback(knockbackDirection);

            // Remember previous direction for knockback
            previousDirection = knockbackDirection;
        }
    }

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
}
