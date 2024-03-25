using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirItens : Missao {
    public GameObject[] itens;

    public static DestruirItens instance;

    private int numDestruido;
    // Start is called before the first frame update
    void Start()
    {
        if (!DestruirItens.instance) {
            DestruirItens.instance = this;
        }
    }

    public void atualizaMissao() {
        numDestruido = 0;
        foreach(GameObject item in itens) {
            if (!item || !item.activeSelf) {
                numDestruido++;
            }
        }
        if (numDestruido == itens.Length) {
            this.Finish();
        }
    }

    public override string GetText() {
        return $"Objetivo: Destrua 7 jarros de argila escondidos ({numDestruido}/{itens.Length}) \nRecompensa: Recupera 1 de vida por segundo";
    }

    public override void Finish() {
        //Ativação da recompensa
    }
}
