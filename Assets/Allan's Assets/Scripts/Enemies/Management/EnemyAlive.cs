using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAlive : MonoBehaviour
{
    private Stats stats;
    public static event System.Action<EnemyAlive> OnEnemyKilled;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }
    void Update()
    {
        if (stats.currentHP <= 0)
        {
            Debug.Log(this.name + " died at EnemyAlive!");
            OnEnemyKilled?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
