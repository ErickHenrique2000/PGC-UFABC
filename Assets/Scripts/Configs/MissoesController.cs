using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissoesController : MonoBehaviour
{
    public Missao[] missoes;
    public string mensagem;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {


        mensagem = "Missões secundárias:\n\n";
        foreach(Missao m in missoes) {
            mensagem += m.GetText() + "\n";
        }



        //Debug.Log(mensagem);
    }
}
