using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float speed = 12f;
    [SerializeField] public float damage = 8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deal damage to the player
        if (collision.gameObject.TryGetComponent(out Stats stats))
        {
            collision.gameObject.GetComponent<Stats>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}