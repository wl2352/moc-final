using UnityEngine;
using System;
using System.Collections.Generic;

// Define a class or struct to represent the loot item
[Serializable]
public class LootItem
{
    public string itemName;
    public GameObject lootPrefab;
    public int minAmount;
    public int maxAmount;
}

public class E_Drop : MonoBehaviour
{
    public List<LootItem> lootTable = new List<LootItem>(); // List of loot items with their details

    public void DropLoot()
    {
        foreach (LootItem lootItem in lootTable)
        {
            int lootAmount = UnityEngine.Random.Range(lootItem.minAmount, lootItem.maxAmount + 1); // Randomize loot amount

            for (int i = 0; i < lootAmount; i++)
            {
                Vector3 dropPosition = transform.position + Vector3.up; // Offset drop position slightly above the enemy
                GameObject lootDrop = Instantiate(lootItem.lootPrefab, dropPosition, Quaternion.identity);
                // Optionally, you can add logic to customize the loot drop further (e.g., different loot types, randomize loot properties, etc.)
            }
        }
    }
}
