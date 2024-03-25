using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMagicShot : Projectile
{
    private float xStartPosition;
    [SerializeField]
    private int damage;

    private bool hasHitGround;

    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.velocity = transform.right * speed;

        xStartPosition = transform.position.x;

        attackDetails.damageAmount = damage;

        hasHitGround = false;
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                damageHit.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
                //TODO: HIT ANIMATION
            }

            if (groundHit)
            {
                Destroy(gameObject);
                //TODO: HIT ANIMATION
            }

            if (Mathf.Abs(xStartPosition - transform.position.x) >= travelDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
