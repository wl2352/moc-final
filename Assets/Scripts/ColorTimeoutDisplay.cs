using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorTimeoutDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private P_ColorSwitch ps;
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        ps = FindObjectOfType<P_ColorSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.colorCooldownTimer <= 0f)
        {
            textMeshProUGUI.text = "";
        }
        else
        {
            textMeshProUGUI.text = $"{Mathf.Round(ps.colorCooldownTimer)}";
        }
    }
}
