using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    //public Animator         animator;
    public SpriteRenderer   sprite;
    GroundEnemy             enemyController;
    public GameObject       enemyFire;
    public  Transform       fireSpawn;
    public  Transform       playerT;

    public Vector3          fireSpawnPos;
    public float            fireRange;
    public float            nextAttack;
    public float            attackRate;
    void Start()
    {
        enemyController = GetComponent<GroundEnemy>();
        sprite = GetComponent<SpriteRenderer>();
        playerT = GameObject.Find("personagem").GetComponent<Transform>();
        enemyController.isRanged = true;
        fireSpawnPos = fireSpawn.localPosition;
    }
    void Flip()
    {
        if(!sprite.flipX)
        {
            fireSpawn.localPosition = new Vector3(-fireSpawnPos.x,fireSpawnPos.y,fireSpawnPos.z);
        }else
        {
            fireSpawn.localPosition = fireSpawnPos;
        }
        if(!sprite.flipX)
        {
            fireSpawn.position = new Vector3(this.transform.position.x + 0.5f,fireSpawn.position.y,fireSpawn.position.z);
        }else
        {
            fireSpawn.position = new Vector3(this.transform.position.x - 0.5f,fireSpawn.position.y,fireSpawn.position.z);
        }
    }
    void Update()
    {
        
        if(enemyController.distance <= fireRange)
        {
            if(Time.time >= nextAttack)
            {
                RangedAttack();
                nextAttack = Time.time + 1f/attackRate;
            }
            if((transform.position.x > playerT.position.x && sprite.flipX) || (transform.position.x < playerT.position.x && !sprite.flipX))
            {
                Flip();
            }
        }
    }
    void RangedAttack()
    {
        GameObject cloneEnemyFire = Instantiate(enemyFire,fireSpawn.position,fireSpawn.rotation);
        if(sprite.flipX)
        {
            cloneEnemyFire.transform.eulerAngles = new Vector3(0,0,180);
        }
    }
}