using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimator : MonoBehaviour
{
    Animator animator;
    E_Movement enemyMovement;
    E_Attack enemyAttack;
    SpriteRenderer sprite_renderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        enemyMovement = FindObjectOfType<E_Movement>();
        enemyAttack = FindObjectOfType<E_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyMovement.direction == Vector3.zero)
        {
            Debug.Log("is idling");
            animator.SetBool("isAggro", false);
        }
        else
        {
            animator.SetBool("isAggro", true);
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
