using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemiesLeft : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    [SerializeField] List<E_Stats> enemies = new List<E_Stats>();

    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectsOfType<E_Stats>().ToList();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = FindObjectsOfType<E_Stats>().ToList();
        textMeshProUGUI.text = enemies.Count.ToString();
    }
}
