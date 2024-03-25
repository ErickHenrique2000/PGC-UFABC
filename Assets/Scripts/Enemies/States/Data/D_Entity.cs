using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")] 
public class D_Entity : ScriptableObject
{
    public bool canBeStun = true;
    public bool canDodge = false;

    public int maxHealth = 30, layer = 10;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;
    
    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;
    public float closeRangeActionDistance = 1f;

    public float damageHopSpeed = 10f;
    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float visionAngle;

    public Vector2 minVisionSize;
    public Vector2 maxVisionSize;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}