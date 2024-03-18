using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMovement player_movement;
    SpriteRenderer sprite_renderer;
    PlayerStats player_stats;
    bool isIdle = true;
    // Start is called before the first frame update
    void Start()
    {
        player_stats = FindObjectOfType<PlayerStats>();
        animator = GetComponent<Animator>();
        player_movement = GetComponent<PlayerMovement>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Idle Orientation
        if (player_movement.last_moved_vector.x == 0 && player_movement.last_moved_vector.y > 0 && isIdle)
        {
            animator.SetBool("Diff_Idle_Up", true);
            animator.SetBool("Diff_Idle_Down", false);
            animator.SetBool("Diff_Idle_UD", false);
            animator.SetBool("Diff_Idle_DD", false);
        }
        else if (player_movement.last_moved_vector.x == 0 && player_movement.last_moved_vector.y < 0 && isIdle)
        {
            animator.SetBool("Diff_Idle_Down", true);
            animator.SetBool("Diff_Idle_Up", false);
            animator.SetBool("Diff_Idle_UD", false);
            animator.SetBool("Diff_Idle_DD", false);
        }
        else if (player_movement.last_moved_vector.x != 0 && player_movement.last_moved_vector.y > 0 && isIdle)
        {
            animator.SetBool("Diff_Idle_UD", true);
            animator.SetBool("Diff_Idle_Up", false);
            animator.SetBool("Diff_Idle_Down", false);
            animator.SetBool("Diff_Idle_DD", false);
        }
        else if (player_movement.last_moved_vector.x != 0 && player_movement.last_moved_vector.y < 0 && isIdle)
        {
            animator.SetBool("Diff_Idle_DD", true);
            animator.SetBool("Diff_Idle_Up", false);
            animator.SetBool("Diff_Idle_Down", false);
            animator.SetBool("Diff_Idle_UD", false);
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

        if (player_movement.movement_dir.x == 0 && player_movement.movement_dir.y > 0)
        {
            animator.SetBool("Move_Up", true);
            isIdle = false;
        }
        else
        {
            animator.SetBool("Move_Up", false);
            isIdle = true;
        }

        if (player_movement.movement_dir.x == 0 && player_movement.movement_dir.y < 0)
        {
            animator.SetBool("Move_Down", true);
            isIdle = false;
        }
        else
        {
            animator.SetBool("Move_Down", false);
            isIdle = true;
        }

        if (player_movement.movement_dir.x != 0 && player_movement.movement_dir.y > 0)
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
        }


        // Stationary Attacking
        if (player_movement.last_moved_vector.x != 0 && player_movement.last_moved_vector.y == 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_H", true);
            player_stats.isAttacking = true;
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_H", false);
                player_stats.isAttacking = false;
            }
        }

        if (player_movement.last_moved_vector.x == 0 && player_movement.last_moved_vector.y > 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_Up", true);
            player_stats.isAttacking = true;
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_Up", false);
                player_stats.isAttacking = false;
            }
        }

        if (player_movement.last_moved_vector.x == 0 && player_movement.last_moved_vector.y < 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_Down", true);
            player_stats.isAttacking = true;
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_Down", false);
                player_stats.isAttacking = false;
            }
        }

        if (player_movement.last_moved_vector.x != 0 && player_movement.last_moved_vector.y > 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_UD", true);
            player_stats.isAttacking = true;
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_UD", false);
                player_stats.isAttacking = false;
            }
        }

        if (player_movement.last_moved_vector.x != 0 && player_movement.last_moved_vector.y < 0 && Input.GetMouseButtonDown(0))
        {
            DisableIdleOrientations();
            animator.SetBool("Attack_DD", true);
            player_stats.isAttacking = true;
        }
        else
        {
            if (isAnimationFinished())
            {
                animator.SetBool("Attack_DD", false);
                player_stats.isAttacking = false;
            }
        }

        // Change color -- FOR TESTING PURPOSES ONLY
        switch (player_stats.currState)
        {
            case "red":
                sprite_renderer.color = Color.red; break;
            case "yellow":
                sprite_renderer.color = Color.yellow; break;
            case "blue":
                sprite_renderer.color = Color.blue; break;
            default:
                sprite_renderer.color = Color.white; break;
        }

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
