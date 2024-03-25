using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSeekStateData", menuName = "Data/State Data/Seek State")]
public class D_SeekState : ScriptableObject
{
    public float speed = 2f;
    public float nextWayPointDistance = 3f;
}
