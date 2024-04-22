using UnityEngine;

public class P_Movement : MonoBehaviour
{
    private Stats stats;
    private Rigidbody2D rb;
    private void Start()
    {
        // Find the Stats script attached to the same GameObject
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (stats.currentHP <= 0){
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * stats.Speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
