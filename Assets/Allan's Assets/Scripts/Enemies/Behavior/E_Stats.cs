using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class E_Stats : MonoBehaviour
{
    SpriteRenderer rend;
    public Color currColor;
    Dictionary<string, Color> colors;

    public float health, damage, speed;

    public static event System.Action<E_Stats> OnEnemyKilled;

    [SerializeField]
    List<GameObject> loot = new List<GameObject>();
    [SerializeField]
    float lootRate = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        EnemyStatSetter();
        colors = new Dictionary<string, Color>()
        {
            { "red", Color.red },
            { "yellow", Color.yellow },
            { "blue", Color.blue }
        };
        rend.color = colors[colors.Keys.ToList()[Random.Range(0, colors.Keys.ToList().Count)]];
        currColor = rend.color;
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
            float num = Random.Range(0, 10);
            if (num <= lootRate)
            {
                Instantiate(loot[Random.Range(0, loot.Count)], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }
}
