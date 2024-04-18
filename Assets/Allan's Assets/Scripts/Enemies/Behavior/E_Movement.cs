using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Movement : MonoBehaviour
{
    public float moveSpeed = 0f;
    public float followDistance = 5f;
    public Transform target;
    [HideInInspector]
    public Vector2 E_direction;
    public Vector2 E_velocity;
    private Rigidbody2D enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerStats>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer <= followDistance && GetComponent<E_Attack>().canAttack) 
            {
                E_direction = (target.position - transform.position).normalized;

                E_velocity = E_direction * moveSpeed;

                enemy.velocity = E_velocity;
            }

            else 
            {
                E_velocity = Vector2.zero;
                enemy.velocity = E_velocity;
            }
        }
        else 
        {
            E_velocity = Vector2.zero;
            enemy.velocity = E_velocity;
        }

    }
}
