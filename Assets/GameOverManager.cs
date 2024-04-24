using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void SwitchToMainMenu()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(3);
    }
}
