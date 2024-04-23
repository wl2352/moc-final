using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public GameManager GameManager;
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        GameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        switch (gameObject.name)
        {
            case "TimeoutCost":
                textMeshProUGUI.text = "Timeout" +
                    $"\nCost: {GameManager.timeoutCost}";
                break;
            case "RedCost":
                textMeshProUGUI.text = "Red Upgrade" +
                    $"\nCost: {GameManager.redCost}";
                break;
            case "YellowCost":
                textMeshProUGUI.text = "Yellow Upgrade" +
                    $"\nCost: {GameManager.yellowCost}";
                break;
            case "BlueCost":
                textMeshProUGUI.text = "Blue Upgrade" +
                    $"\nCost: {GameManager.blueCost}";
                break;
            default:
                Debug.LogError("Not a valid gameobject");
                break;
        }
    }
}
