using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

/// <summary>
/// The ColorMatch class handles the color matching logic that is applied to the
/// game's props and enemies ONLY. Note that if this gets attached to the player, the
/// color matching script won't function properly.
/// </summary>
public class ColorMatch : MonoBehaviour
{
    SpriteRenderer myColor;
    SpriteRenderer otherColor;
    PlayerMovement playerMovement;
    PlayerAnimator playerAnimator;
    PlayerStats ps;
    EnemyMovement es;
    
    public List<GameObject> loot = new List<GameObject>();
    public CinemachineVirtualCamera virtualCamera; // Reference to Cinemachine Virtual Camera

    // Start is called before the first frame update
    void Start()
    {
        ps = FindObjectOfType<PlayerStats>();
        es = FindObjectOfType<EnemyMovement>();
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //playerAnimator = GetComponent<PlayerAnimator>();
        if (col.gameObject.tag == "Player")
        {
            Color myColor = GetComponent<SpriteRenderer>().color;
            Color otherColor = col.gameObject.GetComponent<SpriteRenderer>().color;

            // If colliding with the right color equipped
            if (myColor.Equals(otherColor))
            {
                if (gameObject.tag == "Wolf" && ps.isAttacking)
                {
                    // Checks current mob type -- im too lazy to make this efficient :(
                    if (gameObject.GetComponent<EnemyMovement>() != null)
                    {
                        es.health -= ps.damage;
                        if (es.health <= 0)
                        {
                            Destroy(gameObject);
                        }
                    }
                    // Trigger camera shake
                    ShakeCamera();
                }
            }

            // If the wrong color is equipped on collision
            else
            {
                // And the player collides with an enemy, the player will lose HP
                if (gameObject.tag == "Enemy")
                {
                    float enemydamage = 0f;
                    if (gameObject.GetComponent<EnemyMovement>() != null)
                    {
                        enemydamage = es.damage;
                    }
                   
                    ps.health -= enemydamage;
                    Debug.Log("Player Health: " + ps.health.ToString());
                    // Trigger camera shake
                    ShakeCamera();
                    
                }
            }
        }
        else if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Prop")
        {
            Collider2D thisone = gameObject.GetComponent<Collider2D>();
            Collider2D other = col.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(thisone, other);
        }
    }

    void ShakeCamera()
    {
        // Check if the virtual camera is not null
        if (virtualCamera != null)
        {
            // Get the Cinemachine component for camera shake
            CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            // Check if the CinemachineBasicMultiChannelPerlin component is not null
            if (noise != null)
            {
                // Modify the noise properties to induce camera shake
                noise.m_AmplitudeGain = 1f; // Adjust this value as needed
                noise.m_FrequencyGain = 10f; // Adjust this value as needed
                                             // You can modify other properties as needed for different shake effects
            }
        }
    }
}
