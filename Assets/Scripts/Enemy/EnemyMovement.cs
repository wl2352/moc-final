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
    public float damage, health, speed, cooldown, ttCooldown;
    public bool isAttacking, isHowling, isDead, isIdle, isRunning;

    // Enemy movement & collision
    Rigidbody2D rb;
    Transform player;
    Vector2 movementDir;
    [HideInInspector]
    public float last_horizontal_vector;
    [HideInInspector]
    public float last_vertical_vector;
    [HideInInspector]
    public Vector2 movement_dir;
    [HideInInspector]
    public Vector2 last_moved_vector;
    [SerializeField]
    public float awareness;

    private PlayerAwareness _playerAwareness;

    Dictionary<string, Color> colors;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        _playerAwareness = FindObjectOfType<PlayerAwareness>();
        player = FindObjectOfType<PlayerMovement>().transform;

        // Gets a random color
        colors = new Dictionary<string, Color>()
        {
            { "red", Color.red },
            { "yellow", Color.yellow },
            { "blue", Color.blue }
        };
        EnemyStatSetter();

        ttCooldown = cooldown;
        enemyColorState = colors.Keys.ToList()[Random.Range(0, colors.Keys.ToList().Count)];
        rend.color = colors[enemyColorState];
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerAwareness.AwareOfPlayer)
        {
            isIdle = false;
            isRunning = true;
            if (transform.position.x > player.position.x)
            {
                last_horizontal_vector = -1;
                last_moved_vector = new Vector2(last_horizontal_vector, 0f);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x <= player.position.x)
            {
                last_horizontal_vector = 1;
                last_moved_vector = new Vector2(last_horizontal_vector, 0f);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (transform.position.y > player.position.y)
            {
                last_vertical_vector = -1;
                last_moved_vector = new Vector2(0f, last_vertical_vector);
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            if (transform.position.y <= player.position.y)
            {
                last_vertical_vector = 1;
                last_moved_vector = new Vector2(0f, last_vertical_vector);
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            if (last_horizontal_vector != 0 && last_vertical_vector != 0)
            {
                last_moved_vector = new Vector2(last_horizontal_vector, last_vertical_vector);
            }

            if (Vector2.Distance(transform.position, player.position) < 0.5f)
            {
                isAttacking = true;
                isRunning = false;
                ttCooldown -= Time.deltaTime;
                if (ttCooldown <= 0)
                {
                    ttCooldown = cooldown;
                    isAttacking = false;
                }
                _playerAwareness.AwareOfPlayer = false;
            }
            else
            {
                isAttacking = false;
                isRunning = false;
            }
        }
        else
        {
            isIdle = true;
            isRunning = false;
        }
    }

    void EnemyStatSetter()
    {
        switch(gameObject.tag)
        {
            case "Wolf":       
                damage = 2.0f;
                health = 5.0f;
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


}
