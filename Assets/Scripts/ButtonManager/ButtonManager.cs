using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject currentPanel;
    public bool isOpen = false;
    public string panelTag = "Panel";

    public void TogglePanel(GameObject panelToOpen)
    {
        isOpen = !isOpen; // Toggle the boolean value

        // Find the currently active panel with the specified tag
        GameObject currentPanel = GameObject.FindWithTag(panelTag);

        if (currentPanel != null)
        {
            currentPanel.SetActive(false); // Close the current panel
        }

        if (panelToOpen != null)
        {
        
            panelToOpen.SetActive(isOpen); // Open the new panel
        }
        else
        {
            Debug.LogWarning("Panel to open is not assigned!");
        }
    }

    public void PlayIntroBtn()
    {
        SceneManager.LoadScene("intro scene"); // menu to intro scene
    }

    public void PlayGameBtn()
    {
        SceneManager.LoadScene("Demo"); // menu to intro scene
    }
    public void RestartBtn()
    {
        Time.timeScale = 1; // Ensure time scale is set to 1 before reloading scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    public void OverworldBtn()
    {
        SceneManager.LoadScene("Overworld"); // overworld from menu
    }

    public void QuitGameBtn()
    {
        Application.Quit(); // Exits the game. You can't hit the quit button while in Unity Engine. 
    }

}
