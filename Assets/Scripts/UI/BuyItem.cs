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
                p_ColorSwitch.DecreaseCooldown();
                break;
            case 1:
                playerStats.OnPurchase(gameManager.redCost);
                p_ColorSwitch.IncrementColorLevel("red");
                break;
            case 2:
                playerStats.OnPurchase(gameManager.yellowCost);
                p_ColorSwitch.IncrementColorLevel("yellow");
                break;
            case 3: 
                playerStats.OnPurchase(gameManager.blueCost);
                p_ColorSwitch.IncrementColorLevel("blue");
                break;
            case 4:
                playerStats.OnPurchase(gameManager.healthCost);
                playerStats.IncreaseHealth(10.0f);
                break;
        }
    }
}
