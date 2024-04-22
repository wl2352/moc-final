using UnityEngine;

public class E_Colors : MonoBehaviour
{
    
    public Color color = Color.white; // option for red, blue, and yellow colors
    public float buff = 10f; // Amount by which the stat increases when affected by color
    public float debuff = 10f; // Amount by which the stat decreases when affected by color
    private Stats stats;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private void Start()
    {
        stats = GetComponent<Stats>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the SpriteRenderer component is assigned
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not assigned!");
            return;
        }

        // Assign the chosen color to the enemy
        /*switch (color.ToLower())
        {
            case "red":
                spriteRenderer.color = Color.red;
                break;
            case "blue":
                spriteRenderer.color = Color.blue;
                break;
            case "yellow":
                spriteRenderer.color = Color.yellow;
                break;
            default:
                Debug.LogWarning("Invalid color choice. Defaulting to black.");
                spriteRenderer.color = Color.black;
                break;
        }*/

        ApplyColorEffect(color);
    }

    public void ApplyColorEffect(Color color)
    {
        spriteRenderer.color = color;
        // Apply different effects based on color
        if (color == Color.red)
        {
            // Increase attack stat temporarily
            stats.Attack += stats.Attack * (buff / 100f);
            // Decrease defense stat temporarily
            stats.Defense -= stats.Defense * (debuff / 100f);
        }
        else if (color == Color.blue)
        {
            // Increase defense stat temporarily
            stats.Defense += stats.Defense * (buff / 100f);
            // Decrease speed stat temporarily
            stats.Speed -= stats.Speed * (debuff / 100f);
        }
        else if (color == Color.yellow)
        {
            // Increase speed stat temporarily
            stats.Speed += stats.Speed * (buff / 100f);
            // Decrease attack stat temporarily
            stats.Attack -= stats.Attack * (debuff / 100f);
        }
    }
}