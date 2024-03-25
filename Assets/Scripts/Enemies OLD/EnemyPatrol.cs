using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform        groundCheck;
    public GroundEnemy      controller;
    public bool             patrolling;
    public Vector3          groundCheckPos;
    public SpriteRenderer   spriteRenderer;
    void Start()
    {
        controller = GetComponent<GroundEnemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundCheckPos = groundCheck.localPosition;
    }  
    void Update()
    {
        if(!controller.isMoving)
        {
            patrolling = Physics2D.Linecast(transform.position,groundCheck.position,1 << LayerMask.NameToLayer("chao"));
            if(patrolling)
            {
                controller.Movement();
            }else
            {
                controller.Flip();
            }
        }
        if(spriteRenderer.flipX)
        {
            groundCheck.localPosition = new Vector3(-groundCheckPos.x,groundCheckPos.y,groundCheckPos.z);

        }else
        {
            groundCheck.localPosition = groundCheckPos;
        }
    }
}