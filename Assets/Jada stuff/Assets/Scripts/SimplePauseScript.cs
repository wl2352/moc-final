using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePauseScript : MonoBehaviour
{
    public GameObject Maingame;
    public GameObject PauseUI;

    public bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // Pause or resume time

        Maingame.SetActive(!isPaused); // Enable or disable main game objects
        PauseUI.SetActive(isPaused); // Enable or disable pause UI
        AudioListener.pause = isPaused; // Pause or resume audio
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Ensure time scale is set to 1 before reloading scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1; // Ensure time scale is set to 1 before quitting
        SceneManager.LoadScene(0); // Load an empty scene
        Application.Quit(); // Qu
    }
}
