using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float movementSpeed = 1.0f;
    Vector2 movement_dir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        float moveX, moveY;
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        movement_dir = new Vector2(moveX, moveY).normalized;
        rb.velocity = movement_dir * movementSpeed;
    }
}
