using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMage_LookForPlayerState : LookForPlayerState
{
    private BasicMage enemy;

    public BasicMage_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, BasicMage enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            if(enemy.isPatrol)
            {
                stateMachine.ChangeState(enemy.moveState);
            }
            else
            {
                enemy.idleState.setFlipAfterIdle(false);
                stateMachine.ChangeState(enemy.idleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SetTurnImmediately(bool flip)
    {
        base.SetTurnImmediately(flip);
    }
}
