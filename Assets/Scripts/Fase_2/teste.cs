using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour
{
    public  bool        playerNoLocal;
    public  bool        inimigosNoLocal;
    public  bool        EmCombate;
    public  float       range;
    public  LayerMask   inimigos; 
    public  LayerMask   playerMask;
    public  GameObject  porta;
    public  Vector2     rangeBox;
    public  double      alturaMin;
    public  double      alturaMax;
    public  float       velocidade;
    public  float       atual;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        atual = porta.transform.position.y;
        //inimigosNoLocal = Physics2D.OverlapCircle(transform.position, range, inimigos);
        playerNoLocal = Physics2D.OverlapBox(transform.position, rangeBox, 0, playerMask);
        if(inimigosNoLocal && playerNoLocal){
            EmCombate = true;
        }else{
            EmCombate = false;
        }

        /*if (EmCombate){
            porta.SetActive(true);
        }else{
            porta.SetActive(false);
        }*/

        controleDaPorta(EmCombate);
    }

    void controleDaPorta(bool EmCombate){
        if(EmCombate){
            if(porta.transform.position.y <= alturaMax){
                porta.transform.position = new Vector3(porta.transform.position.x, porta.transform.position.y + velocidade*Time.deltaTime, porta.transform.position.z);
            }
        } else {
            if(porta.transform.position.y >= alturaMin){
                porta.transform.position = new Vector3(porta.transform.position.x, porta.transform.position.y - velocidade * Time.deltaTime, porta.transform.position.z);
            }
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireCube(transform.position,rangeBox);
    }
}
