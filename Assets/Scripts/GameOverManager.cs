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
        PlayerPrefs.DeleteKey("LevelsCleared");
        PlayerPrefs.DeleteKey("Attack");
        PlayerPrefs.DeleteKey("Defense");
        PlayerPrefs.DeleteKey("Speed");
        PlayerPrefs.DeleteKey("CurrentHP");
        PlayerPrefs.DeleteKey("RedLevel");
        PlayerPrefs.DeleteKey("YellowLevel");
        PlayerPrefs.DeleteKey("BlueLevel");
        PlayerPrefs.DeleteKey("ColorCooldown");
        SceneManager.LoadScene(3);
    }
}
