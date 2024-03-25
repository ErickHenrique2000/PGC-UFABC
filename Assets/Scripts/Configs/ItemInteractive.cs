using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractive : MonoBehaviour, Iinteractive
{
    public void interaction() {
        gameObject.SetActive(false);
        DestruirItens.instance.atualizaMissao();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D info) {
        if (info.gameObject.layer == 0) {
            
            this.interaction(); //chama as ativações que desejamos realizar
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("OnCollisionEnter2D");
    }
}
