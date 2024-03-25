using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleportState : TeleportState
{
    protected D_SimpleTeleportState stateData;
    private Transform teleportPosition;

    public SimpleTeleportState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform teleportPosition, D_SimpleTeleportState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        this.teleportPosition = teleportPosition;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Teleport()
    {
        base.Teleport();

        entity.aliveGO.transform.position = teleportPosition.position;
    }
}
