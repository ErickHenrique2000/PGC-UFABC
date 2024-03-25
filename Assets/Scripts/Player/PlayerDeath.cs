using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public static PlayerDeath Instance;
    public GameObject LancaPrefab;
    public GameObject EscudoPrefab;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Kill()
    {
        GameObject.Find("Skills").SetActive(false);
        GameObject Lanca = Instantiate(LancaPrefab, gameObject.transform.position, Quaternion.identity);
        GameObject Escudo = Instantiate(EscudoPrefab, gameObject.transform.position, Quaternion.identity);
        int lado = NewPlayerScript.Player.ladoD ? 1 : -1;
        Lanca.GetComponent<Rigidbody2D>().velocity = Vector2.right * lado * 2;
        Escudo.GetComponent<Rigidbody2D>().velocity = Vector2.right * lado * 2;
    }
}
