using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BFlyEnemy : Entity
{
    public BFlyEnemy_IdleState idleState { get; private set; }
    public BFlyEnemy_PlayerDetectedState playerDetectedState { get; private set; }
    public BFlyEnemy_SeekState seekState { get; private set; }
    public BFlyEnemy_HitState hitState { get; private set; }
    public BFlyEnemy_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_SeekState seekStateData;
    [SerializeField]
    private D_HitState hitStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    public Seeker seeker;

    public override void Start()
    {
        base.Start();

        seeker = transform.GetComponent<Seeker>();

        idleState = new BFlyEnemy_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new BFlyEnemy_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        seekState = new BFlyEnemy_SeekState(this, stateMachine, "seek", seekStateData, seeker, this);
        hitState = new BFlyEnemy_HitState(this, stateMachine, "hit", hitStateData, this);
        deadState = new BFlyEnemy_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(idleState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            deadState.SetDeathAnim(deadStateData.hasDeathAnim);
            stateMachine.ChangeState(deadState);
        }
        else if (stateMachine.currentState != hitState && hitStateData.hasHitAnim)
        {
            stateMachine.ChangeState(hitState);
        }
    }
}
