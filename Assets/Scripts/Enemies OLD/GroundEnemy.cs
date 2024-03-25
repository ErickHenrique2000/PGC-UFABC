using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
    public bool         isRanged;
    public float        Agrorange;
    public float        speed;
    
    protected override void Update()
    {
        base.Update();
    }

    public void Flip()
    {
        sprite.flipX = !sprite.flipX;
        speed*=-1;
    }

    void FixedUpdate()
    {
        if(isMoving && isRanged && distance < Agrorange)
        {
            if((transform.position.x < player.position.x && sprite.flipX) || (transform.position.x > player.position.x && !sprite.flipX))
            {
                Flip();
            }
            Movement();
        }else if(isMoving && !isRanged && distance > Agrorange)
        {   
            if((transform.position.x < player.position.x && sprite.flipX) || (transform.position.x > player.position.x && !sprite.flipX))
            {
                Flip();
            }
            Movement();
        }else
        {
            animator.SetBool("andar",false);
        }
    }

    public void Movement()
    {
        animator.SetBool("andar",true);
        if(isRanged)
        {
            rb2d.velocity = new Vector2(-speed,0f);
        }else
        {
            rb2d.velocity = new Vector2(speed,0f);
        }
    }
}