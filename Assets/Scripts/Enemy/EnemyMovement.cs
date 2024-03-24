using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // THIS CLASS IS CURRENTLY JUST A DUMP FOR ENEMY SHTUFF

    // Stuff for color state of enemy
    SpriteRenderer rend;
    public Color RandomColor;
    public Color red = Color.red;
    public Color yellow = Color.yellow;
    public Color blue = Color.blue;
    public string enemyColorState;

    // Enemy stats
    public float damage, health, speed;

    // Enemy movement & collision
    Rigidbody2D rb;
    GameObject player;
    Vector2 movementDir;
    // EnemyTypes?
    enum enemyTypes
    {
        BRUTE, FLYER, WIZARD
    }
    enemyTypes type;

    Dictionary<string, Color> colors;

    EnemyMovement(int eType)
    {
        EnemyTypeSetter(eType);
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        // Gets a random color
        colors = new Dictionary<string, Color>()
        {
            { "red", Color.red },
            { "yellow", Color.yellow },
            { "blue", Color.blue }
        };

        enemyColorState = colors.Keys.ToList()[Random.Range(0, colors.Keys.ToList().Count)];
        rend.color = colors[enemyColorState];     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyTypeSetter(int n = -1)
    {
        if (n == -1) n = Random.Range(0, 3);
        switch (n)
        {
            case 0:
                type = enemyTypes.BRUTE;
                damage = 2.0f;
                health = 10.0f;
                speed = 1.0f;
                break;
            case 1:
                type = enemyTypes.FLYER;
                damage = 1.0f;
                health = 3.0f;
                speed = 3.0f;
                break;
            case 2:
                type = enemyTypes.WIZARD;
                damage = 2.0f;
                health = 5.0f;
                speed = 1.0f;
                break;
            default:
                return;
        }
    }


}
