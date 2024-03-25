using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FckingHorse : Enemy
{
    protected override void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        if(isMoving)
        {
            rb2d.velocity = new Vector2(moveSpeed,0f);
            animator.SetBool("andar",true);
        }
    }
}
