using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMage : Entity
{
    public BasicMage_IdleState idleState { get; private set; }
    public BasicMage_MoveState moveState { get; private set; }
    public BasicMage_PlayerDetectedState playerDetectedState { get; private set; }
    public BasicMage_LookForPlayerState lookForPlayerState { get; private set; }
    public BasicMage_RangedAttackState rangedAttackState { get; private set; }
    public BasicMage_MeleeAttackState meleeAttackState { get; private set; }
    public BasicMage_StunState stunState { get; private set; }
    public BasicMage_HitState hitState { get; private set; }
    public BasicMage_TeleportState teleportState { get; private set; }
    public BasicMage_DeadState deadState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_StunStateData stunStateData;
    [SerializeField]
    public D_SimpleTeleportState teleportStateData;
    [SerializeField]
    public D_DeadState deadStateData;
    [SerializeField]
    private D_RangedAttackData rangedAttackStateData;
    [SerializeField]
    private D_HitState hitStateData;

    [SerializeField]
    private Transform meleeAttackPoint;
    [SerializeField]
    private Transform rangedAttackPoint;
    [SerializeField]
    private Transform teleportPoint;

    public float meleeAttackRate;
    public float meleeNextAttack;

    public float rangedAttackRate;
    public float rangedNextAttack;

    public override void Start()
    {
        base.Start();

        idleState = new BasicMage_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new BasicMage_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new BasicMage_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new BasicMage_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        rangedAttackState = new BasicMage_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPoint, rangedAttackStateData, this);
        meleeAttackState = new BasicMage_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPoint, meleeAttackStateData, this);
        hitState = new BasicMage_HitState(this, stateMachine, "hit", hitStateData, this);
        stunState = new BasicMage_StunState(this, stateMachine, "stun", stunStateData, this);
        teleportState = new BasicMage_TeleportState(this, stateMachine, "teleport", teleportPoint, teleportStateData, this);
        deadState = new BasicMage_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            deadState.SetDeathAnim(deadStateData.hasDeathAnim);
            stateMachine.ChangeState(deadState);
        }
        else if (!isStunned && stateMachine.currentState != hitState && hitStateData.hasHitAnim)
        {
            stateMachine.ChangeState(hitState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if (CheckPlayerInMinAgroRange())
        {
            if (Time.time >= rangedNextAttack)
            {
                stateMachine.ChangeState(rangedAttackState);
            }
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void Respawn()
    {
        base.Respawn();

        idleState.setFlipAfterIdle(false);
        stateMachine.ChangeState(idleState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeAttackStateData.attackRadius);
    }
}
