using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHazard : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(0, 7, true);

        collision.gameObject.GetComponent<PlayerStats>().TakeDamage(0.1f);
    }
}
