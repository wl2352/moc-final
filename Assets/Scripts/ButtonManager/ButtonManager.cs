﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public bool isOpen = false;
    public string panelTag = "Panel";

    public GameObject pausePanel;
    public GameObject infoPanel;
    public AudioSource audioSource; 
    public AudioClip buttonClickSound; 


    public void TogglePanel(GameObject panelToOpen)
    {
        // Close the pause panel if it's open
        if (pausePanel != null && pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }

        // Close the currently active panel
        GameObject currentPanel = GameObject.FindWithTag(panelTag);
        if (currentPanel != null && currentPanel != infoPanel)
        {
            currentPanel.SetActive(false);
        }

        // Open the new panel
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Panel to open is not assigned!");
        }
    }

public void PlayNextDialogue()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        audioSource.PlayOneShot(buttonClickSound);

    }
    public void PlayIntroBtn()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // menu to intro scene
    }

    public void PlayGameBtn()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("Level 0 [Updated]"); // menu to intro scene
    }
    public void RestartBtn()
    {
        Time.timeScale = 1; // Ensure time scale is set to 1 before reloading scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioSource.PlayOneShot(buttonClickSound);


    }
    public void OverworldBtn()
    {
        SceneManager.LoadScene(2); // overworld from menu
    }

    public void QuitGameBtn()
    {
        Application.Quit(); // Exits the game. You can't hit the quit button while in Unity Engine. 
    }

}
