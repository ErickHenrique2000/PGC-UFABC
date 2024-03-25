using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public GroundEnemy          enemyController;
    public LayerMask            playerLayer;
    public int                  damage;
    public float                nextAttack;
    public float                attackRate;
    public Vector2              attackRange;
    public float                attackAngle;


    void Start()
    {
        enemyController = GetComponent<GroundEnemy>();
        enemyController.isRanged = false;
        enemyController.Agrorange = attackRange.x/2;
    }

    void Update()
    {
        if(Time.time >= nextAttack)
        {
            MeleeAttack();
            nextAttack = Time.time + 1f / attackRate;
        }
    }

    void MeleeAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(transform.position,attackRange,attackAngle,playerLayer);
        foreach(Collider2D player in hitPlayer)
        {
            if(player.GetComponent<PersonagemPrincipal>().invencivel == false)
            {
                if(GetComponent<SpriteRenderer>().flipX)
                {
                    player.GetComponent<PersonagemPrincipal>().ladoInimigo = -1;
                }else
                {   
                    player.GetComponent<PersonagemPrincipal>().ladoInimigo = 1;   
                }
                player.GetComponent<PersonagemPrincipal>().vidaAtual-= damage;
                player.GetComponent<PersonagemPrincipal>().sofreuDano = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position,attackRange);
    }
}
