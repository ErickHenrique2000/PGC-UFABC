using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;


public class fogo_grego : MonoBehaviour
{
    public  float           velocidade;
    public  float           resultante;
    public  float           lado;
    public  float           tempoMax;
    public  float           tempo;
    public  int             damage = 1;
    public  Transform       check;
    public  AttackDetails   attackDetails;
    public  NewPlayerScript player;
    public  Animator        animator;
    public  Light2D         luz;
    private bool            apagar;

    // Start is called before the first frame update
    public void Start()
    {
        //lado = PersonagemPrincipal.ladostatic;
        player = FindObjectOfType<NewPlayerScript>();
        //lado = player.lado;
        tempo = 0;
        if(player.lado == 0){
            lado = 1;
            player.lado = 1;
        }
        /*if((lado != -1 && player.lado == -1) || (lado != 1 && player.lado == 1)) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }*/
        if(transform.localScale.x < 0) {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        transform.localScale = new Vector2(transform.localScale.x * player.lado, transform.localScale.y);
        lado = player.lado;
        animator = this.gameObject.GetComponent<Animator>();
        luz = GameObject.Find("Luzinha").GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        if(luz == null) {
            print("NULL");
        }
        apagar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(tempo>=tempoMax){
            animator.SetBool("morreu", true);
            apagar = true;
        }
        tempo += Time.deltaTime;
        resultante = velocidade*Time.deltaTime*lado;
        transform.position = new Vector3(transform.position.x + resultante, transform.position.y, transform.position.z);
        if (apagar) {
            luz.intensity -= (6.8f) * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D (Collider2D info)
    {
        if (info.gameObject.layer == 10)
        {
            attackDetails.damageAmount = damage;
            attackDetails.position = transform.position;
            attackDetails.stunDamageAmount = 1;
            info.transform.parent.SendMessage("Damage", attackDetails);
        }
/*        Debug.Log("oi");

        if (info != null)
        {
            Destroy(gameObject);
        }   */

        
    }

    public void DestroyFogo() {
        //Destroy(transform.gameObject);
        gameObject.SetActive(false);
    }
    
}
