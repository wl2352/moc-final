using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMovement player_movement;
    SpriteRenderer sprite_renderer;
    P_Attack player_attack;
    P_ColorSwitch player_colorSwitch;
    bool isIdle = true;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioClip attackClip;

    // Start is called before the first frame update
    void Start()
    {
        player_attack = FindObjectOfType<P_Attack>();
        animator = GetComponent<Animator>();
        player_movement = player_attack.GetComponent<PlayerMovement>();
        sprite_renderer = player_attack.GetComponent<SpriteRenderer>();
        player_colorSwitch = player_attack.GetComponent<P_ColorSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        // Idle Orientation
        if (player_movement.last_moved_vector.y > 0 && isIdle)
        {
            animator.SetBool("Diff_Idle_Up", true);
            animator.SetBool("Diff_Idle_Down", false);
            animator.SetBool("Diff_Idle_UD", false);
            animator.SetBool("Diff_Idle_DD", false);
        }
        else if (player_movement.last_moved_vector.y < 0 && isIdle)
        {
            animator.SetBool("Diff_Idle_Down", true);
            animator.SetBool("Diff_Idle_Up", false);
            animator.SetBool("Diff_Idle_UD", false);
            animator.SetBool("Diff_Idle_DD", false);
        }
        else
        {
            DisableIdleOrientations();
        }

        // Active Player Movement
        if (player_movement.movement_dir.x != 0 && player_movement.movement_dir.y == 0)
        {
            animator.SetBool("Move_H", true);
            isIdle = false;
        }
        else
        {
            animator.SetBool("Move_H", false);
            isIdle = true;
        }

        if (player_movement.movement_dir.y > 0)
        {
            animator.SetBool("Move_Up", true);
            isIdle = false;
        }
        else
        {
            animator.SetBool("Move_Up", false);
            isIdle = true;
        }

        if (player_movement.movement_dir.y < 0)
        {
            animator.SetBool("Move_Down", true);
            isIdle = false;
        }
        else
        {
            animator.SetBool("Move_Down", false);
            isIdle = true;
        }

        /*if (player_movement.movement_dir.x != 0 && player_movement.movement_dir.y > 0)
        {
            animator.SetBool("Move_UD", true);
            isIdle = false;
        }
        else
        {
            animator.SetBool("Move_UD", false);
            isIdle = true;
        }

        if (player_movement.movement_dir.x != 0 && player_movement.movement_dir.y < 0)
        {
            animator.SetBool("Move_DD", true);
            isIdle = false;
        }
        else
        {
            animator.SetBool("Move_DD", false);
            isIdle = true;
        }*/


        // Stationary Attacking
        if (player_movement.last_moved_vector.x != 0 && player_movement.last_moved_vector.y == 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_H", true);
            player_attack.canAttack = false;
            if (player_movement.last_moved_vector.x <= 0)
            {
                attackPoint.transform.position = new Vector3(player_movement.transform.position.x - 1.0f, player_movement.transform.position.y, 0);
            }
            else
            {
                attackPoint.transform.position = new Vector3(player_movement.transform.position.x + 1.0f, player_movement.transform.position.y, 0);
            }
            attackSound.PlayOneShot(attackClip);
            
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_H", false);
                player_attack.canAttack = true;
                //player_attack.atkDur = 0f;
            }
        }

        if (player_movement.last_moved_vector.x == 0 && player_movement.last_moved_vector.y > 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_Up", true);
            player_attack.canAttack = false;
            attackPoint.transform.position = new Vector3(player_movement.transform.position.x, player_movement.transform.position.y + 1.0f, 0);
            attackSound.PlayOneShot(attackClip);
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_Up", false);
                player_attack.canAttack = true;
                //player_attack.atkDur = 0f;
            }
        }

        if (player_movement.last_moved_vector.x == 0 && player_movement.last_moved_vector.y < 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_Down", true);
            player_attack.canAttack = false;
            attackPoint.transform.position = new Vector3(player_movement.transform.position.x, player_movement.transform.position.y - 1.0f, 0);
            attackSound.PlayOneShot(attackClip);
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_Down", false);
                player_attack.canAttack = true;
                //player_attack.atkDur = 0f;
            }
        }

        /*if (player_movement.last_moved_vector.x != 0 && player_movement.last_moved_vector.y > 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_UD", true);
            player_stats.isAttacking = true;
            if (player_movement.last_moved_vector.x <= 0)
            {
                attackPoint.transform.position = new Vector3(player_movement.transform.position.x - 1.0f, player_movement.transform.position.y + 1.0f, 0);
            }
            else
            {
                attackPoint.transform.position = new Vector3(player_movement.transform.position.x + 1.0f, player_movement.transform.position.y + 1.0f, 0);
            }
            attackSound.PlayOneShot(attackClip);
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_UD", false);
                player_stats.isAttacking = false;
                player_stats.atkDur = 0f;
            }
        }

        if (player_movement.last_moved_vector.x != 0 && player_movement.last_moved_vector.y < 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_DD", true);
            player_stats.isAttacking = true;
            if (player_movement.last_moved_vector.x <= 0)
            {
                attackPoint.transform.position = new Vector3(player_movement.transform.position.x - 1.0f, player_movement.transform.position.y - 1.0f, 0);
            }
            else
            {
                attackPoint.transform.position = new Vector3(player_movement.transform.position.x + 1.0f, player_movement.transform.position.y - 1.0f, 0);
            }
            attackSound.PlayOneShot(attackClip);
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_DD", false);
                player_stats.isAttacking = false;
                player_stats.atkDur = 0f;
            }
        }*/

        // Change color -- FOR TESTING PURPOSES ONLY
        /*switch (player_colorSwitch.active)
        {
            case "red":
                sprite_renderer.color = Color.red; break;
            case "yellow":
                sprite_renderer.color = Color.yellow; break;
            case "blue":
                sprite_renderer.color = Color.blue; break;
            default:
                sprite_renderer.color = new Color(.35f, .28f, .28f, 1f); break;
        }*/

        SpriteDirectionCheck();
    }

    void SpriteDirectionCheck()
    {
        if (player_movement.last_horizontal_vector < 0)
        {
            sprite_renderer.flipX = false;
        }
        else
        {
            sprite_renderer.flipX = true;
        }
    }

    bool isAnimationFinished()
    {
        float nTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (nTime > 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DisableIdleOrientations()
    {
        animator.SetBool("Diff_Idle_Up", false);
        animator.SetBool("Diff_Idle_Down", false);
        animator.SetBool("Diff_Idle_UD", false);
        animator.SetBool("Diff_Idle_DD", false);
    }
}
