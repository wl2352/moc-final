using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string overworldSceneName; // Name of the overworld scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check for player interaction (e.g., button press)
            if (Input.GetKeyDown(KeyCode.E)) // Change KeyCode.E to your desired interact key
            {
                // Load the overworld scene
                SceneManager.LoadScene(overworldSceneName);
            }
        }
    }
}
