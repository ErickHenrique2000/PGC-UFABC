using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArcher : Entity
{
    public BasicArcher_IdleState idleState {get; private set; }
    public BasicArcher_MoveState moveState { get; private set; }
    public BasicArcher_PlayerDetectedState playerDetectedState { get; private set; }
    public BasicArcher_LookForPlayerState lookForPlayerState { get; private set; }
    public BasicArcher_RangedAttackState rangedAttackState { get; private set; }
    public BasicArcher_MeleeAttackState meleeAttackState { get; private set; }
    public BasicArcher_StunState stunState { get; private set; }
    public BasicArcher_DodgeState dodgeState { get; private set; }
    public BasicArcher_DeadState deadState { get; private set; }
    public BasicArcher_HitState hitState { get; private set; }

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
    private D_DeadState deadStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    private D_RangedAttackData rangedAttackStateData;
    [SerializeField]
    private D_HitState hitStateData;

    [SerializeField]
    private Transform meleeAttackPoint;
    [SerializeField]
    private Transform rangedAttackPoint;

    public float meleeAttackRate;
    public float meleeNextAttack;

    public float rangedAttackRate;
    public float rangedNextAttack;

    public override void Start()
    {
        base.Start();

        idleState = new BasicArcher_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new BasicArcher_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new BasicArcher_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new BasicArcher_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        rangedAttackState = new BasicArcher_RangedAttackState(this, stateMachine, "rangedAttack", rangedAttackPoint, rangedAttackStateData, this);
        meleeAttackState = new BasicArcher_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPoint, meleeAttackStateData, this);
        stunState = new BasicArcher_StunState(this, stateMachine, "stun", stunStateData, this);
        dodgeState = new BasicArcher_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        deadState = new BasicArcher_DeadState(this, stateMachine, "dead", deadStateData, this);
        hitState = new BasicArcher_HitState(this, stateMachine, "hit", hitStateData, this);

        stateMachine.Initialize(idleState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            deadState.SetDeathAnim(deadStateData.hasDeathAnim);
            stateMachine.ChangeState(deadState);
        }
        else if(!isStunned && stateMachine.currentState != hitState && hitStateData.hasHitAnim)
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
