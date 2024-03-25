using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator          animator;
    protected SpriteRenderer    sprite;
    protected Rigidbody2D       rb2d;
    public int                  maxHealth;  
    public int                  currentHealth;
    public int                  moveSpeed;
    protected Transform         player;
    public float                distanceDetection;
    public float                distance;
    public bool                 isMoving = false;
    void Start()
    {
        player = GameObject.Find("personagem").GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    protected float PlayerDistance()
    {
        return Vector2.Distance(player.position,transform.position);
    }

    /*protected void Flip()
    {
        sprite.flipX = !sprite.flipX;
        moveSpeed*=-1;
    }*/
    protected virtual void Update()
    {
        distance = PlayerDistance();
        isMoving = (distance <= distanceDetection);
        if(currentHealth <= 0)
        {
            Death();
        }
        /*if(isMoving)
        {
            if((transform.position.x < player.position.x && sprite.flipX) || (transform.position.x > player.position.x && !sprite.flipX))
            {
                Flip();
            }
        }*/
    }
    public IEnumerator DamageFx()
    {
        Color curColor = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds (0.1f);
        sprite.color = curColor;
    }
    public void TakeDamage(int damage)
    {
        currentHealth-=damage;
        //Animação de Dano
        //animator.SetTrigger("Hurt");
        StartCoroutine(DamageFx());
        if(currentHealth <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        //Animação De Morte
        //animator.SetBool("IsDead",true);
        Debug.Log("Enemy Died!");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}