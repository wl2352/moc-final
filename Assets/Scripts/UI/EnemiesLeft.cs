using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemiesLeft : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    [SerializeField] List<EnemyAlive> enemies = new List<EnemyAlive>();

    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectsOfType<EnemyAlive>().ToList();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = FindObjectsOfType<EnemyAlive>().ToList();
        textMeshProUGUI.text = enemies.Count.ToString();
    }
}
