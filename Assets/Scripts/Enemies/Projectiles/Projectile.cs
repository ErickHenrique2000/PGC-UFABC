 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected AttackDetails attackDetails;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected float speed;
    protected float travelDistance;
    [SerializeField]
    protected float damageRadius;

    protected bool canHit;


    [SerializeField]
    protected LayerMask whatIsGround;
    [SerializeField]
    protected LayerMask whatIsPlayer;
    [SerializeField]
    protected Transform damagePosition;

    public virtual void FireProjectile(float speed, float travelDistance, int damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }
}
