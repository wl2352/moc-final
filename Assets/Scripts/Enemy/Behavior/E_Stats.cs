using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Stats : MonoBehaviour
{
    public float health, damage, speed;

     public static event System.Action<E_Stats> OnEnemyKilled;

    List<GameObject> loot = new List<GameObject>();
    [SerializeField]
    float lootRate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        EnemyStatSetter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyStatSetter()
    {
        switch(gameObject.tag)
        {
            case "Wolf":       
                damage = 2.0f;
                health = 4.0f;
                speed = 1.0f;
                break;
            case "Flyer":
                damage = 1.0f;
                health = 3.0f;
                speed = 1.5f;
                break;
            default:
                return;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log(this.name + " died!");
            float num = UnityEngine.Random.Range(0, 10);
            if (num <= lootRate)
            {
                Instantiate(loot[UnityEngine.Random.Range(0, loot.Count)], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }
}
