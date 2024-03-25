using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : Enemy
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position,player.position,Mathf.Abs(moveSpeed)*Time.deltaTime);
            animator.SetBool("andar",true);
        }else
        {
            animator.SetBool("andar",false);
        }
    }
}
