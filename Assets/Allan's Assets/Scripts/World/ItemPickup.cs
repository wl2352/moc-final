using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            P_ColorSwitch colorSwitch = other.GetComponent<P_ColorSwitch>(); // Get the P_ColorSwitch script from the player

            if (colorSwitch != null)
            {
                // Enable color unlocks based on the color of the item
                if (gameObject.CompareTag("RedBoost"))
                {
                    colorSwitch.UnlockColor(Color.red);
                }
                else if (gameObject.CompareTag("BlueBoost"))
                {
                    colorSwitch.UnlockColor(Color.blue);
                }
                else if (gameObject.CompareTag("YellowBoost"))
                {
                    colorSwitch.UnlockColor(Color.yellow);
                }

                // Disable the item GameObject
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("P_ColorSwitch script not found on the player GameObject!");
            }
        }
    }
}
