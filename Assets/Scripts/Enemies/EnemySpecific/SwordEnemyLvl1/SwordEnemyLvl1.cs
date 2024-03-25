using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyLvl1 : Entity
{
    public SwordEnemyLvl1_IdleState idleState { get; private set; }
    public SwordEnemyLvl1_MoveState moveState { get; private set; }
    public SwordEnemyLvl1_PlayerDetectedState playerDetectedState { get; private set; }
    public SwordEnemyLvl1_ChargeState chargeState { get; private set; }
    public SwordEnemyLvl1_MeleeAttackState meleeAttackState { get; private set; }
    public SwordEnemyLvl1_LookForPlayerState lookForPlayerState { get; private set; }
    public SwordEnemyLvl1_StunState stunState { get; private set; }
    public SwordEnemyLvl1_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    D_StunStateData stunStateData;
    [SerializeField]
    D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPoint;

    public float meleeAttackRate;
    public float meleeNextAttack;

    public override void Start()
    {
        base.Start();

        idleState = new SwordEnemyLvl1_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new SwordEnemyLvl1_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new SwordEnemyLvl1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new SwordEnemyLvl1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        meleeAttackState = new SwordEnemyLvl1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPoint, meleeAttackStateData, this);
        lookForPlayerState = new SwordEnemyLvl1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new SwordEnemyLvl1_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new SwordEnemyLvl1_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if(isDead)
        {
            deadState.SetDeathAnim(true);
            stateMachine.ChangeState(deadState);
        }
        else if(isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if(!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeAttackStateData.attackRadius);
    }
}