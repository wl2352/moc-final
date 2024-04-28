using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimation : MonoBehaviour
{
    Animator animator;
    E_Movement enemyMovement;
    E_Attack enemyAttack;
    SpriteRenderer sprite_renderer;
    public bool up;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        enemyMovement = FindObjectOfType<E_Movement>();
        enemyAttack = FindObjectOfType<E_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyMovement.direction);
        // State handling
        if (enemyMovement.direction.y > 0)
        {
            up = true;
        }
        else
        {
            up = false;
        }

        if (enemyMovement.direction == Vector3.zero)
        {
            animator.SetBool("RunningUp", false);
            animator.SetBool("Running", false);
            Debug.Log("is idling");
            if (up)
            {
                animator.SetBool("IdleUp", true);
            }
            else
            {
                animator.SetBool("IdleUp", false);
            }

            if (!enemyAttack.canAttack)
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
        }
        else
        {
            animator.SetBool("IdleUp", false);
            animator.SetBool("AttackingUp", false);
            animator.SetBool("Attacking", false);
            
            if (up)
            {
                animator.SetBool("RunningUp", true);
                animator.SetBool("Running", false);
                Debug.Log("is moving up");
            }
            else
            {
                animator.SetBool("Running", true);
                animator.SetBool("RunningUp", false);
                Debug.Log("is moving down");
            }
        }

        SpriteDirectionCheck();
    }

    void SpriteDirectionCheck()
    {
        if (enemyMovement.direction.x < 0)
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