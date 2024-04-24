using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityLevelDisplay : MonoBehaviour
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
        switch (gameObject.name)
        {
            case "Red Level":
                textMeshProUGUI.text = ps.redLevel.ToString();
                break;
            case "Yellow Level":
                textMeshProUGUI.text = ps.yellowLevel.ToString();
                break;
            case "Blue Level":
                textMeshProUGUI.text = ps.blueLevel.ToString();
                break;
            default:
                textMeshProUGUI.text = "";
                break;
        }
    }
}
