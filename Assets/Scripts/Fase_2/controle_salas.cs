using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controle_salas : MonoBehaviour
{
    public  bool    playerNoLocal;
    public  bool    inimigosNoLocal;
    public  bool    EmCombate;
    public  float   range;
    public  LayerMask   inimigos; 
    public  LayerMask   playerMask;
    public  GameObject  porta;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //inimigosNoLocal = Physics2D.OverlapCircle(transform.position, range, inimigos);
        playerNoLocal = Physics2D.OverlapCircle(transform.position, range, playerMask);
        if(inimigosNoLocal && playerNoLocal){
            EmCombate = true;
        }else{
            EmCombate = false;
        }
        if(EmCombate){
            porta.SetActive(true);
        }else{
            porta.SetActive(false);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
