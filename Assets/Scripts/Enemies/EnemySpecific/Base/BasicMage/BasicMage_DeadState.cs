using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMage_DeadState : DeadState
{
    private BasicMage enemy;

    public BasicMage_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, BasicMage enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void AfterDeathAnim()
    {
        base.AfterDeathAnim();
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

        if (!enemy.isDead)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SetDeathAnim(bool hasDeathAnim)
    {
        base.SetDeathAnim(hasDeathAnim);
    }
}
