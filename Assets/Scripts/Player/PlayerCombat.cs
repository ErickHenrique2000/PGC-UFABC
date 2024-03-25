using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public AttackDetails attackDetails;

    public NewPlayerScript player;

    public Animator     animator;

    public LayerMask    enemyLayers;

    public Transform    swordPoint;

    public int swordAttackDamage;
    
    public float        swordAttackRange;
    public float        swordAttRate;
    private float               nextSwordAtt;
    public Transform    spearPoint;
    public Vector2      spearAttackRange;
    public float        spearAttackAngle;
    public int          spearAttackDamage;
    public float        spearAttRate;
    float               nextSpearAtt;
    void Start()
    {
        player = GameObject.Find("personagem").GetComponent<NewPlayerScript>();
    }
    void Update()
    {
        if (Time.time >= nextSwordAtt)
        {
            if (Input.GetButtonDown("espada") && !player.espada)
            {
                animator.SetTrigger("Sword");
                nextSwordAtt = Time.time + 1f / swordAttRate;

                soundControllerScript.PlaySound("espada");
                player.espada = true;
                player.tempoEspada = 0;
            }
        }

        if (Time.time >= nextSpearAtt && player.grounded)
        {
            if (Input.GetButtonDown("lanca") /*&& !jogador.Jump*/)
            {
                animator.SetTrigger("Spear");
                nextSpearAtt = Time.time + 1f / spearAttRate;

                player.lanca = true;
                player.tempoLanca = 0;
                player.tempoSemMover = 0.5f;

            }
        }

        if (player.espada)
        {
            player.tempoEspada += player.tempo;
            if (player.tempoEspada >= 0.25f)
            {
                player.espada = false;
            }
        }

        if (player.lanca)
        {
            player.tempoLanca += player.tempo;

            if (player.tempoLanca >= 0.5f)
            {
                player.lanca = false;
            }
        }  

    }
    public void SwordAttack()
    {
        //Detectar os inimigos
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(swordPoint.position,swordAttackRange,enemyLayers);

        //Dar dano nos inimigos
        attackDetails.position = transform.position;
        attackDetails.damageAmount = swordAttackDamage;
        attackDetails.stunDamageAmount = 1f;
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    public void SpearAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(spearPoint.position,spearAttackRange,spearAttackAngle,enemyLayers);

        attackDetails.position = transform.position;
        attackDetails.damageAmount = spearAttackDamage;
        attackDetails.stunDamageAmount = 1f;
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.transform.parent.SendMessage("Damage", attackDetails);
        }

    }
    void OnDrawGizmosSelected()
    {
        if(swordPoint == null)
            return;
        Gizmos.DrawWireSphere(swordPoint.position,swordAttackRange);

        if(spearPoint == null)
            return;
        Gizmos.DrawWireCube(spearPoint.position,spearAttackRange);
    }
}