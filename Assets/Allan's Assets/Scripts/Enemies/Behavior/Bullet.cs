using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 12f;
    [SerializeField] public float damage = 8f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, collision.gameObject.layer);
        }
        // Deal damage to the player
        if (collision.gameObject.TryGetComponent(out Stats stats))
        {
            collision.gameObject.GetComponent<Stats>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}