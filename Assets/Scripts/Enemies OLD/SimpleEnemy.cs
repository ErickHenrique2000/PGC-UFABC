using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : BasicEnemyController
{   

    void Start()
    {
        if(healthBar != null)
        {
            healthBar.SetMaxHealth(health);
        }  
    }
    protected override void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        if(!isMoving)
        {
            Patrulha();
        }else
        {
            rb2d.velocity = new Vector2(moveSpeed,rb2d.velocity.y);
            if((player.position.x > transform.position.x && transform.localScale.x < 0) || 
				(player.position.x < transform.position.x && transform.localScale.x > 0))
                {
                    Flip();
                }
        }
    }

    void Patrulha()
    {
        rb2d.velocity = new Vector2(moveSpeed,rb2d.velocity.y);
        if(!grounded || wallHit)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        moveSpeed*=-1;
    }
}
