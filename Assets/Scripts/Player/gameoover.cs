using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoover : MonoBehaviour
{
    public FaseController faseAnterior;

    public void MainMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void voltar(){
        faseAnterior = FindObjectOfType<FaseController>();

        if (faseAnterior == null)
        {
            SceneManager.LoadScene("Fase 1");
        } else
        {
            SceneManager.LoadScene(faseAnterior.nomeFase);
        }
        
    }
}
