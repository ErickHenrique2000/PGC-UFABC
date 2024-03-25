using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private float xStartPosition;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float gravity;

    private bool isGravityOn;
    private bool hasHitGround;

    private void Start()
    {
        canHit = true;
        isGravityOn = false;
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        xStartPosition = transform.position.x;

        hasHitGround = false;
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;

            if (isGravityOn && canHit)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {

            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit && canHit)
            {
                canHit = false;
                damageHit.SendMessage("Damage", attackDetails);
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                transform.parent = damageHit.transform.parent;
                Destroy(gameObject, 2f);
            }

            if (groundHit)
            {
                canHit = false;
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                Destroy(gameObject, 2f);
            }

            if (Mathf.Abs(xStartPosition - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
