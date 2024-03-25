using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{   
    public int                  health;
    public float                moveSpeed;
    public float                distanceAttack;

    public bool                 isMoving;
    public bool                 grounded;
    public bool                 wallHit;

    public HealthBar            healthBar;
    public Transform            groundCheck, wallCheck;
    protected Rigidbody2D       rb2d;
    protected Animator          animator;
    protected Transform         player;
    protected SpriteRenderer    sprite;

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    protected float PlayerDistance()
    {
        return Vector2.Distance(player.position,transform.position);
    }

    protected virtual void Update()
    {
        if(groundCheck != null && wallCheck != null)
        {
            grounded = Physics2D.Linecast(transform.position,groundCheck.position,1 << LayerMask.NameToLayer("Ground"));
            wallHit = Physics2D.Linecast(transform.position,wallCheck.position,1 << LayerMask.NameToLayer("Ground"));
        }
        float distance = PlayerDistance();
        isMoving = (distance <= distanceAttack);
    }

    public void DamageEnemy(int damage)
    {
        health-= damage;
        if(healthBar != null)
        {
            healthBar.SetHealth(health);
        }  
        StartCoroutine (DamageAnim());
        if(health < 1)
        {
            Destroy(gameObject);
        }

    }

    public IEnumerator DamageAnim()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}