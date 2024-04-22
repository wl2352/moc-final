using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHazard : MonoBehaviour
{
    public float damageInterval = 2f; // Time interval between damage
    public int damageAmount = 4; // Amount of damage to inflict

    [SerializeField] private float timer; // Timer to track damage intervals
    [SerializeField] private bool playerOnObject; // Flag to track if player is on the object

    private PlayerStats player;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }
    void Update()
    {
        // If player is on the object, start the timer
        if (playerOnObject)
        {
            timer += Time.deltaTime;

            // If the timer exceeds the damage interval, inflict damage and reset the timer
            if (timer >= damageInterval)
            {
                // Call a function to damage the player here
                DamagePlayer();

                // Reset the timer
                timer = 0f;
            }
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Set playerOnObject flag to true
            playerOnObject = true;
        }
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object leaving the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Set playerOnObject flag to false
            playerOnObject = false;
            timer = 0f;
        }
    }

    // Function to damage the player
    void DamagePlayer()
    {
        player.TakeDamage(damageAmount);
        Debug.Log("Player takes " + damageAmount + " damage!");
    }
}
