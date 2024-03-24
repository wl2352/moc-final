using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Animation : MonoBehaviour
{
    private Animator anim;
    public string[] IdleDirections = {"IdleN", "IdleNW", "IdleW", "IdleSW", "IdleS", "IdleSE", "IdleE", "IdleNE"};
    public string[] RunDirections = {"RunN", "RunNW", "RuneW", "RunSW", "RunS", "RunSE", "RunE", "RunNE"};
    int lastDirection;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
        Debug.Log("R1 " + result1);
    }

    // Update is called once per frame
    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray = null;

        if(_direction.magnitude < 0.01)
        {
            directionArray = IdleDirections;
        }
        else
        {
            directionArray = RunDirections;
            lastDirection = DirectionToIndex(_direction);
        }
        anim.Play(directionArray[lastDirection]);
    }

    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;
        float step = 360 / 8;
        float offset = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, norDir);
        angle += offset;
        if(angle < 0) 
        {
            angle += 360;
        }
        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}
