using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBar;
    PlayerStats ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = FindObjectOfType<PlayerStats>();
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = ps.health / 100f;
    }
}
