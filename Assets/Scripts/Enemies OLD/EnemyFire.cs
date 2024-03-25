using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public float                speed;
    public float                timeDestroy;
    public int                  damage;
    public PersonagemPrincipal  player;
    void Start()
    {
        player = GameObject.Find("personagem").GetComponent<PersonagemPrincipal>();
        timeDestroy = 0.5f;
        Destroy(gameObject,timeDestroy);
    }

    void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(GetComponent<SpriteRenderer>().flipX)
            {
                player.ladoInimigo = 1;
            }else
            {
                player.ladoInimigo = -1;
            }
            if(!player.invencivel)
            {
                player.vidaAtual -= damage;
                player.sofreuDano = true;
            }
        }
        Destroy(gameObject);
    }
}
