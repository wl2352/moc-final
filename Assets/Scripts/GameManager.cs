using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool devMode = false;
    public int enemyCount = 0;
    public int maxCount = 5;
    public bool maxReached = false;
    TextMeshProUGUI textMeshProUGUI;
    List<EnemyMovement> enemies = new List<EnemyMovement>();

    private void OnEnable()
    {
        EnemyMovement.OnEnemyKilled += HandleEnemyDefeated;
    }

    private void OnDisable()
    {
        EnemyMovement.OnEnemyKilled -= HandleEnemyDefeated;
    }

    void Start()
    {
        enemies = GameObject.FindObjectsOfType<EnemyMovement>().ToList();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
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
            enemies = GameObject.FindObjectsOfType<EnemyMovement>().ToList();
            enemyCount = enemies.Count;
            if (enemyCount >= maxCount)
            {
                maxReached = true;
            }
            else
            {
                maxReached = false;
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

    void HandleEnemyDefeated(EnemyMovement enemy)
    {
        enemies.Remove(enemy);
    }
}
