using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool devMode = false;
    public int enemyCount = 0;
    TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
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
            textMeshProUGUI.text = enemyCount.ToString();
        }
    }
}
