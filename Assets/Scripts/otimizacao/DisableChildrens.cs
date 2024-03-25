using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableChildrens : MonoBehaviour
{
    public  GameObject  Pai;
    private Transform   Player;
    private int         qtdChilds;

    private void Start()
    {
        Player = NewPlayerScript.Player.transform;
        Pai = this.gameObject;
        qtdChilds = Pai.transform.childCount;
        //Debug.Log(qtdChilds);
    }

    void FixedUpdate()
    {
        for(int i = 0; i < qtdChilds; i++)
        {
            Transform obj = Pai.transform.GetChild(i);
            if(Vector3.Distance(Player.transform.position, obj.position) > 20)
            {
                obj.gameObject.SetActive(false);
            }
            else
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}
