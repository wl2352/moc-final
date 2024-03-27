using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugDialog : MonoBehaviour
{
    TextMeshProUGUI m_DebugGUI;
    PlayerStats m_PlayerStats;
    PlayerMovement m_PlayerMovement;
    GameManager m_GameManager;
    // Start is called before the first frame update
    void Start()
    {
        m_DebugGUI = GetComponent<TextMeshProUGUI>();
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        m_PlayerStats = FindObjectOfType<PlayerStats>();
        m_GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GameManager.devMode)
        {
            if (m_DebugGUI.gameObject.name == "DebugL")
            {
                m_DebugGUI.text = "" +
                            "Current player abilities: " + string.Join(", ", m_PlayerStats.currentStates) + "\n" +
                            "Current state: " + m_PlayerStats.currState.ToString() + "\n" +
                            "x: " + m_PlayerMovement.gameObject.transform.position.x.ToString() + "\t" + "y: " + m_PlayerMovement.gameObject.transform.position.y.ToString() + "\n" +
                            "Slots: " + m_PlayerStats.slots.ToString() + "\n" +
                            "Duration: " + m_PlayerStats.duration.ToString();
            }
            if (m_DebugGUI.gameObject.name == "DebugR")
            {
                m_DebugGUI.text = "Stats: \n" +
                            "Health: " + m_PlayerStats.health.ToString() + "\n" +
                            "Damage: " + m_PlayerStats.damage.ToString() + "\n" +
                            "CritChance: " + m_PlayerStats.critChance.ToString() + "\n" +
                            "Block: " + m_PlayerStats.block.ToString() + "\n" +
                            "Speed: " + m_PlayerStats.movementSpeed.ToString() + "\n" +
                            "Stamnia: " + m_PlayerStats.stamina.ToString() + "\n" +
                            "Luck: " + m_PlayerStats.luck.ToString();
            }
        }
        else
        {
            if (m_DebugGUI.gameObject.name == "Errors")
            {
                m_DebugGUI.text = m_PlayerStats.errorMsg;
            }
            else
            {
                m_DebugGUI.text = "";
            }
            
        }
        
    }
}
