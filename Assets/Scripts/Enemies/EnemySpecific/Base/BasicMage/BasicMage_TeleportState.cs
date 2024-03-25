using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMage_TeleportState : SimpleTeleportState
{
    private BasicMage enemy;
    public BasicMage_TeleportState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform teleportPosition, D_SimpleTeleportState stateData, BasicMage enemy) : base(entity, stateMachine, animBoolName, teleportPosition, stateData)
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

    public override void FinishTeleport()
    {
        base.FinishTeleport();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            enemy.stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Teleport()
    {
        base.Teleport();
    }
}
