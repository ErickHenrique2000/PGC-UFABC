using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArcher_IdleState : IdleState
{
    private BasicArcher enemy;
    public BasicArcher_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, BasicArcher enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (isIdleTimeOver && enemy.isPatrol)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
