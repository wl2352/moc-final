using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerStats stats;
    [SerializeField]
    float sprintSpeedMultiplier = 1.4f;
    public float movementSpeed;
    [HideInInspector]
    public float last_horizontal_vector;
    [HideInInspector]
    public float last_vertical_vector;
    [HideInInspector]
    public Vector2 movement_dir;
    [HideInInspector]
    public Vector2 last_moved_vector;

    public bool canDash = true;
    public bool isDashing;
    float dashingPower = 24f;
    float dashingTime = .2f;
    float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        float moveX, moveY;
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        movement_dir = new Vector2(moveX, moveY).normalized;
        if (isDashing) return;
        if (movement_dir.x != 0)
        {
            last_horizontal_vector = movement_dir.x;
            last_moved_vector = new Vector2(last_horizontal_vector, 0f);
        }
        if (movement_dir.y != 0)
        {
            last_vertical_vector = movement_dir.y;
            last_moved_vector = new Vector2(0f, last_vertical_vector);
        }
        if (movement_dir.x != 0 && movement_dir.y != 0)
        {
            last_moved_vector = new Vector2(last_horizontal_vector, last_vertical_vector);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = movement_dir * stats.movementSpeed * sprintSpeedMultiplier;
        }
        else
        {
            rb.velocity = movement_dir * stats.movementSpeed;
        }

        // Does not work
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        
    }

    // Does not work
    private IEnumerator Dash()
    {
        Debug.Log("reached");
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * last_horizontal_vector * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
}
