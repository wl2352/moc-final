using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    private Stats playerStats;
    private P_ColorSwitch p_ColorSwitch;
    private GameManager gameManager;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        p_ColorSwitch = GameObject.FindGameObjectWithTag("Player").GetComponent<P_ColorSwitch>();
        gameManager = FindObjectOfType<GameManager>();
}
    public void GiveItem(int objID)
    {
        switch (objID)
        {
            case 0:
                if (playerStats.currency >= gameManager.timeoutCost)
                {
                    playerStats.currency -= gameManager.timeoutCost;
                    p_ColorSwitch.colorCooldown = p_ColorSwitch.colorCooldown - (p_ColorSwitch.colorCooldown * 0.1f);
                }
                break;
            case 1:
                if (playerStats.currency >= gameManager.redCost)
                {
                    playerStats.currency -= gameManager.redCost;
                    p_ColorSwitch.redLevel++;
                }
                break;
            case 2:
                if (playerStats.currency >= gameManager.yellowCost)
                {
                    playerStats.currency -= gameManager.yellowCost;
                    p_ColorSwitch.yellowLevel++;
                }
                break;
            case 3:
                if (playerStats.currency >= gameManager.blueCost)
                {
                    playerStats.currency -= gameManager.blueCost;
                    p_ColorSwitch.blueLevel++;
                }
                break;
        }
    }
}
