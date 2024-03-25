using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int      curLevelIndex;
    public int      curMaxHealth;
    public bool     possuiFogo;
    public bool     possuiTp;
    public bool     possuiAguia;
    public PlayerInfo (PersonagemPrincipal player)
    {   
        possuiFogo = player.possuiFogo;
        possuiAguia = player.possuiAguia;
        possuiTp = player.possuitp;
        //COISAS PARA SALVAR
    }
}