using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    PlayerStates allStates;
    GameManager gameManager;
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
    public string errorMsg;

    // Attack Area
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRadius;
    // Start is called before the first frame update
    void Start()
    {
        allStates = FindObjectOfType<PlayerStates>();
        gameManager = FindObjectOfType<GameManager>();
        currentStates = new List<string>()
        {
            "base"
        };
        currState = currentStates[0];
        prevHealth = 100.0f;
        health = 100.0f;
        atkDur = 0f;
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
        
        // Currently bugged and is breaking game
        /*if (gameManager.devMode)
        {
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
        }*/
        

        // Attacking Action
        if (isAttacking && atkDur == 0f)
        {
            Attack();
            atkDur += 1.0f;
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
                errorMsg = "State slots FULL. Slots: " + slots.ToString() + ", State count: " + currentStates.Count.ToString();
            }
        }
        else
        {
            Debug.Log("State already exists");
            errorMsg = "State already exists";
        }

    }

    void ApplyAbility(string ability)
    {
        // Debug.Log(states.states["base"]["damage"]);
        if (!currentStates.Contains(ability)) { Debug.Log("Player does not have " + ability + " ability"); errorMsg = "Player does not have " + ability + " ability";  return; }
        if (ability != "base" && currState == ability) { Debug.Log("Already in " + currState + " state."); errorMsg = "Already in " + currState + " state.";  return; }
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

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            
            // If the damage applied to the enemy can not kill the enemy --> do damage as is
            // If the damage applied to the enemy CAN kill the enemy --> if the colors are the same --> do damage as is
            if (enemy.GetComponent<E_Stats>().health - damage > 0)
            {
                enemy.GetComponent<E_Stats>().TakeDamage(damage);
            }
            else
            {
                if (gameObject.GetComponent<SpriteRenderer>().color == enemy.GetComponent<E_Stats>().currColor)
                {
                    enemy.GetComponent<E_Stats>().TakeDamage(damage);
                }
                else
                {
                    Debug.Log("Cannot kill enemy because they are not the same color");
                    errorMsg = "Cannot kill enemy because they are not the same color";
                }
            }
            

            Debug.Log("After we hit: " + enemy.GetComponent<E_Stats>().health.ToString());
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (currState != "blue") prevHealth = health;
        else
        {
            if (health < prevHealth)
            {
                prevHealth = health;
            }
        }
        if (health <= 0)
        {
            Debug.Log(this.name + " died!");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RedBoost")
        {
            if (currentStates.Contains("red"))
            {
                UpgradeAbility("red", Random.Range(0, 2));
                errorMsg = "Upgraded red ability!\n" +
                    "toggle dev mode to see changes.";
            }
            else
            {
                slots++;
                AddNewState("red");
                errorMsg = "Red granted!";
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "YellowBoost")
        {
            if (currentStates.Contains("yellow"))
            {
                UpgradeAbility("yellow", Random.Range(0, 2));
                errorMsg = "Upgraded yellow ability!\n" +
                    "toggle dev mode to see changes.";
            }
            else
            {
                slots++;
                AddNewState("yellow");
                errorMsg = "Yellow granted!";
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "BlueBoost")
        {
            if (currentStates.Contains("blue"))
            {
                UpgradeAbility("blue", Random.Range(0, 2));
                errorMsg = "Upgraded blue ability!\n" +
                    "toggle dev mode to see changes.";
            }
            else
            {
                slots++;
                AddNewState("blue");
                errorMsg = "Blue granted!";
            }
            Destroy(collision.gameObject);
        }
    }
}
