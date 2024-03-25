using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public NewPlayerScript player;
    public float vida;
    public Image slider;

    private void Start()
    {
        player = NewPlayerScript.Player;
    }

    void FixedUpdate()
    {
        //Debug.Log("t");
        vida = player.GetVida();
        slider.fillAmount = vida / 100;
    }
}
