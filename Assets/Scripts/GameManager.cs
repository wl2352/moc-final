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

    void HandleEnemyDefeated(EnemyMovement enemy)
    {
        enemies.Remove(enemy);
    }
}
