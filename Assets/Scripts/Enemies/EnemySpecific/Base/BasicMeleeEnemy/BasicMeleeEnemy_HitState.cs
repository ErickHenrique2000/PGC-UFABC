using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeEnemy_HitState : HitState
{
    private BasicMeleeEnemy enemy;
    
    public BasicMeleeEnemy_HitState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_HitState stateData, BasicMeleeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (hitOver)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
