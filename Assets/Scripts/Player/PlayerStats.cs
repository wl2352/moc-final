using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerStates allStates;
    public string currState;
    public List<string> currentStates;
    [SerializeField]
    public int slots = 0;
    [SerializeField]
    public float damage, health, block, critChance, stamina, movementSpeed, luck, duration, atkDur;
    [SerializeField]
    public float prevHealth;
    [SerializeField]
    public bool isAttacking = false;
    public GameObject attackPrefab;
    // Start is called before the first frame update
    void Start()
    {
        allStates = FindObjectOfType<PlayerStates>();
        currentStates = new List<string>()
        {
            "base"
        };
        currState = currentStates[0];
        prevHealth = 100.0f;
        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Testing Ability Switching   
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ApplyAbility("red");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ApplyAbility("yellow");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ApplyAbility("blue");
        }
       
        // Testing Adding New Ability
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddNewState("red");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddNewState("yellow");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddNewState("blue");
        }

        // Dev Controls / Manipulate Player Stats
        if (Input.GetKeyDown(KeyCode.Q))
        {
            slots++;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            health -= 1.0f;
            if (currState != "blue") prevHealth = health;
            else
            {
                if (health < prevHealth)
                {
                    prevHealth = health;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            health += 1.0f;
            if (currState != "blue") prevHealth = health;
        }

        // Attacking Action
        // Meant to test if attacking state is tracking properly and also test ability to spawn an object as a result
        // of the player attack. If an item spawns, this means we can spawn some type of collider
        if (isAttacking && GameObject.FindGameObjectWithTag("atk") == null)
        {
            Instantiate(attackPrefab, new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y + 1), Quaternion.identity);
        }
        else if(!isAttacking && GameObject.FindGameObjectWithTag("atk") != null)
        {
            DestroyImmediate(GameObject.FindGameObjectWithTag("atk"), true);
        }

        // Update Durations
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            ApplyAbility("base");
            duration = 0.0f;
        }
    }

    public void AddNewState(string newState)
    {
        if (!currentStates.Contains(newState))
        {
            if (slots > currentStates.Count - 1)
            {
                currentStates.Add(newState);
            }
            else
            {
                Debug.Log("State slots FULL. Slots: " + slots.ToString() + ", State count: " + currentStates.Count);
            }
        }
        else
        {
            Debug.Log("State already exists");
        }

    }

    void ApplyAbility(string ability)
    {
        // Debug.Log(states.states["base"]["damage"]);
        if (!currentStates.Contains(ability)) { Debug.Log("Player does not have " + ability + " ability"); return; }
        if (ability != "base" && currState == ability) { Debug.Log("Already in " + currState + " state."); return; }
        switch (ability)
        {
            case "red":
                if (duration == 0)
                {
                    currState = "red";
                    damage = allStates.states["base"]["damage"] + allStates.states["red"]["damage"];
                    critChance = allStates.states["base"]["critChance"] + allStates.states["red"]["critChance"];
                    duration = allStates.states["red"]["duration"];
                }
                
                break;
            case "yellow":
                if (duration == 0)
                {
                    currState = "yellow";
                    stamina = allStates.states["base"]["stamina"] + allStates.states["yellow"]["stamina"];
                    movementSpeed = allStates.states["base"]["movementSpeed"] + allStates.states["yellow"]["movementSpeed"];
                    duration = allStates.states["yellow"]["duration"];
                }
                
                break;
            case "blue":
                if (duration == 0)
                {
                    currState = "blue";
                    block = allStates.states["base"]["block"] + allStates.states["blue"]["block"];
                    prevHealth = health;
                    if (health + allStates.states["blue"]["health"] > allStates.states["base"]["health"] + allStates.states["blue"]["health"])
                    {
                        health = allStates.states["base"]["health"] + allStates.states["blue"]["health"];
                    }
                    else
                    {
                        health = health + allStates.states["blue"]["health"];
                    }
                    duration = allStates.states["blue"]["duration"];
                }
                
                break;
            default:
                currState = "base";
                if (prevHealth > allStates.states["base"]["health"]) prevHealth = allStates.states["base"]["health"];
                health = prevHealth;
                damage = allStates.states["base"]["damage"];
                block = allStates.states["base"]["block"];
                critChance = allStates.states["base"]["critChance"];
                stamina = allStates.states["base"]["stamina"];
                movementSpeed = allStates.states["base"]["movementSpeed"];
                break;
        }
        // currState = ability;
    }

    void UpgradeAbility(string choice, int stat)
    {
        switch (choice)
        {
            case "red":
                if (stat == 0)
                {
                    allStates.UpdateRed(1.0f, 0.0f);
                }
                else { allStates.UpdateRed(0.0f, 1.0f); } 
                break;
            case "yellow":
                if (stat == 0)
                {
                    allStates.UpdateYellow(1.0f, 0.0f);
                }
                else { allStates.UpdateYellow(0.0f, 1.0f); }
                break;
            case "blue":
                if (stat == 0)
                {
                    allStates.UpdateBlue(1.0f, 0.0f);
                }
                else { allStates.UpdateBlue(0.0f, 1.0f); }
                break;
        }
    }
}
