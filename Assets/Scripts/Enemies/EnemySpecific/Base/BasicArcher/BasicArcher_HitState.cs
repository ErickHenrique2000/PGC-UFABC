using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArcher_HitState : HitState
{
    private BasicArcher enemy;
    public BasicArcher_HitState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_HitState stateData, BasicArcher enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if(hitOver)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
