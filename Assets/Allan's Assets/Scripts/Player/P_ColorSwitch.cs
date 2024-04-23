using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class P_ColorSwitch : MonoBehaviour
{
    [Header("Color Stats")]
    public float colorDuration = 3f; // Duration for which the color effect lasts
    public float buff = 10f; // Amount by which the stat increases when affected by color
    public float debuff = 10f; // Amount by which the stat decreases when affected by color
    public float colorCooldown = 5f; // Cooldown between color switches
    public int redLevel = 1;
    public int yellowLevel = 0;
    public int blueLevel = 0;

    private Stats playerStats; // Reference to the player's Stats script
    [HideInInspector] public Color currentColor; // Current color of the player
    [HideInInspector] public Color activeColor; // Active color that is affecting the player
    [HideInInspector] public Color baseColor = new Color(.35f, .28f, .28f, 1f); // Base color for the player
    [HideInInspector] public float colorEffectTimer = 0f; // Timer to track color effect duration
    private bool colorEffectActive = false; // Flag to track if color effect is active
    private float TempAtk = 0f;
    private float TempDef = 0f;
    private float TempSpd = 0f;
    private bool canSwitchColor = true; // Flag to track if color switching is allowed
    [HideInInspector] public float colorCooldownTimer = 0f; // Timer to track color switch cooldown

    [Space(3f)]
    [Header("Unlocked Colors")]
    public bool redUnlocked = false; // Flag to track if red color is unlocked
    public bool blueUnlocked = false; // Flag to track if blue color is unlocked
    public bool yellowUnlocked = false; // Flag to track if yellow color is unlocked

    private void Start()
    {
        // Find the player's Stats script
        playerStats = GetComponent<Stats>();

        TempAtk = playerStats.Attack;
        TempDef = playerStats.Defense;
        TempSpd = playerStats.Speed;

        // Set the initial color to white (no color)
        currentColor = baseColor;
        activeColor = baseColor;

        // Unlock red color by default
        redUnlocked = true;
        redLevel = 1;
        yellowLevel = 0;
        blueLevel = 0;
    }

    private void Update()
    {
        // Update color switch cooldown timer
        if (!canSwitchColor)
        {
            colorCooldownTimer -= Time.deltaTime;
            if (colorCooldownTimer <= 0f)
            {
                canSwitchColor = true;
                colorCooldownTimer = 0f;
            }
        }

        // Check if the color effect is active
        if (colorEffectActive)
        {
            // Increment the timer
            colorEffectTimer += Time.deltaTime;

            // Check if the color effect duration has elapsed
            if (colorEffectTimer >= colorDuration)
            {
                // Reset color effect
                colorEffectActive = false;
                colorEffectTimer = 0f;

                // Reset player's color to white (no color)
                SetPlayerColor(baseColor);

                // Reset the previously affected stat if any
                ResetTempStatEffect();

                // Start color switch cooldown
                canSwitchColor = false;
                colorCooldownTimer = colorCooldown;
            }
        }

        // Check for input to switch colors if color switch is allowed and color effect is not active
        if (canSwitchColor && !colorEffectActive)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && redUnlocked)
            {
                SwitchColor(Color.red);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && yellowUnlocked)
            {
                SwitchColor(Color.yellow);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && blueUnlocked)
            {
                SwitchColor(Color.blue);
            }
        }
    }

    // Method to switch player color
    private void SwitchColor(Color newColor)
    {
        // Set the new active color
        activeColor = newColor;

        // Set player color
        SetPlayerColor(newColor);

        // Apply color effect
        ApplyColorEffect(newColor);
    }

    // Method to set player color
    private void SetPlayerColor(Color newColor)
    {
        // Change player's material color
        GetComponent<SpriteRenderer>().color = newColor;
        currentColor = newColor;
    }

    // Method to apply color effect
    private void ApplyColorEffect(Color color)
    {
        // Apply different effects based on color
        if (color == Color.red)
        {
            // Increase attack stat temporarily
            playerStats.Attack += playerStats.Attack * ((buff * redLevel) / 100f);
            // Decrease defense stat temporarily
            playerStats.Defense -= playerStats.Defense * (debuff / 100f);
        }
        else if (color == Color.blue)
        {
            // Increase defense stat temporarily
            playerStats.Defense += playerStats.Defense * ((buff * blueLevel) / 100f);
            // Decrease speed stat temporarily
            playerStats.Speed -= playerStats.Speed * (debuff / 100f);
        }
        else if (color == Color.yellow)
        {
            // Increase speed stat temporarily
            playerStats.Speed += playerStats.Speed * ((buff * yellowLevel) / 100f);
            // Decrease attack stat temporarily
            playerStats.Attack -= playerStats.Attack * (debuff / 100f);
        }

        // Set color effect active
        colorEffectActive = true;
    }

    // Method to reset temporary stat modifiers
    public void ResetTempStatEffect()
    {
        playerStats.Attack = TempAtk;
        playerStats.Defense = TempDef;
        playerStats.Speed = TempSpd;
    }

    // Method to unlock colors (could be called when conditions are met)
    public void UnlockColor(Color color)
    {
        if (color == Color.red)
        {
            redUnlocked = true;
            redLevel = 1;
        }
        else if (color == Color.blue)
        {
            blueUnlocked = true;
            blueLevel = 1;
        }
        else if (color == Color.yellow)
        {
            yellowUnlocked = true;
            yellowLevel = 1;
        }
    }

    // Decrease cooldown by 10%
    public void DecreaseCooldown()
    {
        colorCooldown = colorCooldown - (colorCooldown * .10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RedBoost")
        {
            if (redUnlocked)
            {
                redLevel++;
            }
            else
            {
                UnlockColor(Color.red);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "YellowBoost")
        {
            if (yellowUnlocked)
            {
                yellowLevel++;
            }
            else
            {
                UnlockColor(Color.yellow);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "BlueBoost")
        {
            if (blueUnlocked)
            {
                blueLevel++;
            }
            else
            {
                UnlockColor(Color.blue);
            }
            Destroy(collision.gameObject);
        }
    }
}
