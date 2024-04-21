using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class P_ColorSwitch : MonoBehaviour
{
    public float colorDuration = 3f; // Duration for which the color effect lasts
    public float buff = 10f; // Amount by which the stat increases when affected by color
    public float debuff = 10f; // Amount by which the stat decreases when affected by color
    public float colorCooldown = 5f; // Cooldown between color switches

    private Stats playerStats; // Reference to the player's Stats script
    private Color currentColor; // Current color of the player
    private Color activeColor; // Active color that is affecting the player
    private Color baseColor = new Color(.35f, .28f, .28f, 1f); // Base color for the player
    private float colorEffectTimer = 0f; // Timer to track color effect duration
    private bool colorEffectActive = false; // Flag to track if color effect is active
    private float TempAtk = 0f;
    private float TempDef = 0f;
    private float TempSpd = 0f;
    private bool canSwitchColor = true; // Flag to track if color switching is allowed
    private float colorCooldownTimer = 0f; // Timer to track color switch cooldown

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
            else if (Input.GetKeyDown(KeyCode.Alpha2) && blueUnlocked)
            {
                SwitchColor(Color.blue);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && yellowUnlocked)
            {
                SwitchColor(Color.yellow);
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
            playerStats.Attack += buff;
            // Decrease defense stat temporarily
            playerStats.Defense -= debuff;
        }
        else if (color == Color.blue)
        {
            // Increase defense stat temporarily
            playerStats.Defense += buff;
            // Decrease speed stat temporarily
            playerStats.Speed -= debuff;
        }
        else if (color == Color.yellow)
        {
            // Increase speed stat temporarily
            playerStats.Speed += buff;
            // Decrease attack stat temporarily
            playerStats.Attack -= debuff;
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
        }
        else if (color == Color.blue)
        {
            blueUnlocked = true;
        }
        else if (color == Color.yellow)
        {
            yellowUnlocked = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RedBoost")
        {
            if (redUnlocked)
            {
                // Increase buff for RED
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
                // Increase buff for YELLOW
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
                // Increase buff for BLUE
            }
            else
            {
                UnlockColor(Color.blue);
            }
            Destroy(collision.gameObject);
        }
    }
}
