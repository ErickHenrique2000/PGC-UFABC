using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeEnemy : Entity
{
    public BasicMeleeEnemy_IdleState idleState { get; private set; }
    public BasicMeleeEnemy_MoveState moveState { get; private set; }
    public BasicMeleeEnemy_PlayerDetectedState playerDetectedState { get; private set; }
    public BasicMeleeEnemy_ChargeState chargeState { get; private set; }
    public BasicMeleeEnemy_LookForPlayerState lookForPlayerState { get; private set; }
    public BasicMeleeEnemy_MeleeAttackState meleeAttackState { get; private set; }
    public BasicMeleeEnemy_HitState hitState { get; private set; }
    public BasicMeleeEnemy_StunState stunState { get; private set; }
    public BasicMeleeEnemy_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayeStaterData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_HitState hitStateData;
    [SerializeField]
    private D_StunStateData stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    [SerializeField]
    private Difficulty difficultyToEnable;

    public float meleeAttackRate;
    public float meleeNextAttack;

    public override void Start()
    {

        var relation = DifficultyController.instance.getDifficulty().CompareTo(this.difficultyToEnable);
        
        if(relation < 0) {
            Debug.Log(relation);
            Destroy(this.gameObject);
        }

        //... resto do código




        //Debug.Log(this.difficultyToEnable);
        //Debug.Log(DifficultyController.instance.getDifficulty());
        base.Start();

        moveState = new BasicMeleeEnemy_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new BasicMeleeEnemy_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new BasicMeleeEnemy_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new BasicMeleeEnemy_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new BasicMeleeEnemy_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayeStaterData, this);
        meleeAttackState = new BasicMeleeEnemy_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new BasicMeleeEnemy_StunState(this, stateMachine, "stun", stunStateData, this);
        hitState = new BasicMeleeEnemy_HitState(this, stateMachine, "hit", hitStateData, this);
        deadState = new BasicMeleeEnemy_DeadState(this, stateMachine, "dead", deadStateData, this);

        idleState.setFlipAfterIdle(false);
        stateMachine.Initialize(idleState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
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
            if (Time.time >= meleeNextAttack)
            {
                stateMachine.ChangeState(meleeAttackState);
            }
        }
        else if(!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }
}
