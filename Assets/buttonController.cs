using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;

public class buttonController : MonoBehaviour
{
    public Animator start;
    public float    timer;
    private bool comeca;
    public FaseController faseAnterior;

    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("teste").GetComponent<Animator>();
        timer = 0;
        comeca = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (comeca) {
            timer += Time.deltaTime;
            if(timer >= 0.8f) {
                faseAnterior = FindObjectOfType<FaseController>();

                if (faseAnterior == null) {
                    SceneManager.LoadScene("Fase 2");
                } else {
                    SceneManager.LoadScene(faseAnterior.nomeFase);
                }
            }
        }
    }

    void OnMouseOver() {
        start.SetBool("sobre", true);
        print("oi");
    }

    private void OnMouseExit() {
        start.SetBool("sobre", false);
    }

    private void OnMouseDown() {
        start.SetBool("precionado", true);
        comeca = true;
    }
}
