using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liveCharacter : MonoBehaviour
{
    public float vida;

    public virtual void GetDamage(float Damage) {
        vida -= Damage;
    }
}
