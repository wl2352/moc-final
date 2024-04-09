using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimation : MonoBehaviour
{
    Animator animator;
    E_Movement enemyMovement;
    SpriteRenderer sprite_renderer;
    public bool up;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<E_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        // State handling
        if (enemyMovement.E_direction.y > 0)
        {
            up = true;
        }
        else
        {
            up = false;
        }

        if (enemyMovement.isIdle)
        {
            if (up)
            {
                animator.SetBool("IdleUp", true);
                //animator.SetBool("Idle", false);
            }
            else
            {
                //animator.SetBool("Idle", true);
                animator.SetBool("IdleUp", false);
            }

        }
        else
        {
            animator.SetBool("IdleUp", false);
        } 

        if (enemyMovement.isAttacking)
        {
            if (up)
            {
                animator.SetBool("AttackingUp", true);
                animator.SetBool("Attacking", false);
            }
            else
            {
                animator.SetBool("Attacking", true);
                animator.SetBool("AttackingUp", false);
            }
        }
        else
        {
            animator.SetBool("AttackingUp", false);
            animator.SetBool("Attacking", false);
        }

        if (enemyMovement.isHowling)
        {
            if (up)
            {
                animator.SetBool("HowlingUp", true);
                animator.SetBool("Howling", false);
            }
            else
            {
                animator.SetBool("Howling", true);
                animator.SetBool("HowlingUp", false);
            }
        }
        else
        {
            animator.SetBool("HowlingUp", false);
            animator.SetBool("Howling", false);
        }

        if (enemyMovement.isRunning)
        {
            if (up)
            {
                animator.SetBool("RunningUp", true);
                animator.SetBool("Running", false);
            }
            else
            {
                animator.SetBool("Running", true);
                animator.SetBool("RunningUp", false);
            }
        }
        else
        {
            animator.SetBool("RunningUp", false);
            animator.SetBool("Running", false);
        }

        SpriteDirectionCheck();
    }

    void SpriteDirectionCheck()
    {
        if (enemyMovement.E_direction.x < 0)
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
}
