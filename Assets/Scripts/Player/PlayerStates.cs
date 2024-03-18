using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    // States for the player
    /*public class BaseState : States
    {
        public float damage = 1.0f,
            health = 100.0f,
            block = 0.5f,
            critChance = 0.01f,
            stamina = 10.0f,
            movementSpeed = 2.0f,
            luck = 0.01f;
    }
    public class RedState : States
    {
        public float damage = 1.0f,
            critChance = 0.2f;
        public void UpgradeState(float dmg,  float crit)
        {
            damage += dmg;
            critChance += crit;
        }
    }
    public class BlueState : States
    {
        public float block = 2.0f,
            health = 10.0f;
        public void UpgradeState(float blk, float hp)
        {
            block += blk;
            health += hp;
        }
    }
    public class YellowState : States
    {
        public float stamina = 2.0f,
            movementSpeed = 1.0f;
        public void UpgradeState(float stam, float speed)
        {
            stamina += stam;
            movementSpeed += speed;
        }
    }*/

    public Dictionary<string, Dictionary<string, float>> states;

    // Initialize States
    PlayerStats stats;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
        states = new Dictionary<string, Dictionary<string, float>>()
        {
            {
                "base",
                new Dictionary<string, float>()
                    {
                        { "damage", 1.0f },
                        { "health", 100.0f },
                        { "block", 0.5f },
                        { "critChance", 0.0f},
                        { "stamina", 5.0f },
                        { "movementSpeed", 2.0f },
                        { "luck", 0.01f },
                        { "duration", 0.0f }
                    }
            },
            {
                "red",
                new Dictionary<string, float>()
                    {
                        { "damage", 1.0f },
                        { "critChance", 0.2f},
                        { "duration", 3.0f }
                    }
            },
            {
                "yellow",
                new Dictionary<string, float>()
                    {
                        { "stamina", 2.0f },
                        { "movementSpeed", 1.0f },
                        { "duration", 3.0f }
                    }
            },
            {
                "blue",
                new Dictionary<string, float>()
                    {
                        { "health", 10.0f },
                        { "block", 2.0f },
                        { "duration", 3.0f }
                    }
            }
        };
    }

    private void Update()
    {
        
    }


    // Update States
    public void UpdateRed(float damage, float critChance)
    {
        states["red"]["damage"] += damage;
        states["red"]["critChance"] += critChance;
    }

    public void UpdateYellow(float stamina, float movementSpeed)
    {
        states["yellow"]["stamina"] += stamina;
        states["yellow"]["movementSpeed"] += movementSpeed;
    }

    public void UpdateBlue(float health, float block)
    {
        states["blue"]["health"] += health;
        states["blue"]["block"] += block;
    }
    

}
