using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool devMode = false;
    TextMeshProUGUI textMeshProUGUI;
    List<E_Stats> enemies = new List<E_Stats>();

    private void OnEnable()
    {
        E_Stats.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        E_Stats.OnEnemyKilled -= HandleEnemyDefeated;
    }

    void Start()
    {
        enemies = GameObject.FindObjectsOfType<E_Stats>().ToList();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(4);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                devMode = !devMode;
            }
            if (gameObject.name == "Enemies Left")
            {
                textMeshProUGUI.text = enemies.Count.ToString();
            }
            if (FindObjectsOfType<PlayerStats>().ToList().Count == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(4);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                devMode = !devMode;
            }
        }

        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(4);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                devMode = !devMode;
            }
        }

        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(4);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                devMode = !devMode;
            }
        }*/
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            GameControls();
        }

        else 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneManager.LoadScene(3);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    void HandleEnemyDefeated(E_Stats enemy)
    {
        enemies.Remove(enemy);
    }

    void GameControls()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(4);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            devMode = !devMode;
        }
        if (gameObject.name == "Enemies Left")
        {
            textMeshProUGUI.text = enemies.Count.ToString();
        }
        if (FindObjectsOfType<PlayerStats>().ToList().Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
