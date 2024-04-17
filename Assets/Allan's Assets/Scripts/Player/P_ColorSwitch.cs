using UnityEngine;

public class PlayerColorSwitch : MonoBehaviour
{
    public float colorDuration = 3f; // Duration for which the color effect lasts
    public float boost = 10f; // Amount by which the stat increases when affected by color

    private Stats playerStats; // Reference to the player's Stats script
    private Color currentColor; // Current color of the player
    private float colorEffectTimer = 0f; // Timer to track color effect duration
    private bool colorEffectActive = false; // Flag to track if color effect is active

    private void Start()
    {
        // Find the player's Stats script
        playerStats = GetComponent<Stats>();

        // Set the initial color to white (no color)
        currentColor = Color.white;
    }

    private void Update()
    {
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
                SetPlayerColor(Color.white);
            }
        }

        // Check for input to switch colors
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchColor(Color.red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchColor(Color.blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchColor(Color.yellow);
        }
    }

    // Method to switch player color
    private void SwitchColor(Color newColor)
    {
        // Set player color
        SetPlayerColor(newColor);

        // Apply color effect
        ApplyColorEffect(newColor);
    }

    // Method to set player color
    private void SetPlayerColor(Color newColor)
    {
        // Change player's material color
        GetComponent<Renderer>().material.color = newColor;
        currentColor = newColor;
    }

    // Method to apply color effect
    private void ApplyColorEffect(Color color)
    {
        // Reset the previously affected stat if any
        ResetTempStatEffect();

        // Apply different effects based on color
        if (color == Color.red)
        {
            // Increase attack stat temporarily
            TempIncreaseStat(playerStats.Attack, boost);
        }
        else if (color == Color.blue)
        {
            // Increase defense stat temporarily
            TempIncreaseStat(playerStats.Defense, boost);
        }
        else if (color == Color.yellow)
        {
            // Increase speed stat temporarily
            TempIncreaseStat(playerStats.Speed, boost);
        }

        // Set color effect active
        colorEffectActive = true;
    }

    // Temporary stat modifiers
    private float tempstatModifier = 0f;

    // Method to temporarily increase stats
    public void TempIncreaseStat(float stat, float amount)
    {
        tempstatModifier = stat + amount;
        stat = tempstatModifier;
    }

    // Method to reset temporary stat modifiers
    public void ResetTempStatEffect()
    {
        tempstatModifier = 0f;
    }
}
