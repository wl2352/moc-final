using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldSceneLoader : MonoBehaviour
{
    private SpriteRenderer sr;

    public int level;
    [SerializeField] private float timeInterval = 2f;
    [SerializeField] private float timer; // Timer to track damage intervals
    [SerializeField] private bool playerOnObject; // Flag to track if player is on the object

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // If player is on the object, start the timer
        if (playerOnObject)
        {
            timer += Time.deltaTime;

            sr.color = new Color(0.5f, 0.5f, 0.5f);

            // If the timer exceeds the damage interval, inflict damage and reset the timer
            if (timer >= timeInterval)
            {
                // Call a function to damage the player here
                SceneManager.LoadScene(level);

                // Reset the timer
                timer = 0f;
            }
        }
        else
        {
            sr.color = Color.white;
        }
        Color tmp = sr.color;
        tmp.a = 0.22f;
        sr.color = tmp;
    }

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
}
