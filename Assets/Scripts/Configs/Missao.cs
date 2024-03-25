using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missao: MonoBehaviour 
{
    public abstract string GetText();
    public abstract void Finish();
}
