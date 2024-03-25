using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public GroundEnemy enemy;
    public PersonagemPrincipal  player;

    void Start()
    {
            SaveLoad.SavePlayer(player);
            Debug.Log("Salvei");
            player.loadGameStats();
    }


    void Update()
    {
    }
}
