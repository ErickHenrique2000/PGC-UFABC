using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : BasicEnemyController
{
    public GameObject       bulletObject;
    public Transform        bulletSpawn;
    public float            fireRate;
    public float            nextFire;

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
            rb2d.velocity = Vector2.zero;
            if(Time.time>=nextFire)
            {
                Fire();
                nextFire = Time.time + 1f / fireRate;
            }
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

    void Fire()
    {
        GameObject cloneBullet = Instantiate(bulletObject,bulletSpawn.position,bulletSpawn.rotation);
        if(transform.localScale.x < 0)
        {
            cloneBullet.transform.eulerAngles = new Vector3(0,0,180);
        }
        cloneBullet.tag = "EnemyFire";
        cloneBullet.layer = 13;
    }
}
