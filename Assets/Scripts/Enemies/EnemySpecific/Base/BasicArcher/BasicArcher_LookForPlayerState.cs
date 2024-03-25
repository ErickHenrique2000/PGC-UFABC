using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArcher_LookForPlayerState : LookForPlayerState
{
    private BasicArcher enemy;
    public BasicArcher_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, BasicArcher enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            }else
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
