using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSimpleTeleportStateData", menuName = "Data/State Data/Simple Teleport State")]
public class D_SimpleTeleportState : ScriptableObject
{
    public float teleportCooldown = 1f;
}
