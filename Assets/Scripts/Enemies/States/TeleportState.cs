using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State
{
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    public TeleportState(Entity entity, FiniteStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
        
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.teleportState = this;
        isAnimationFinished = false;
        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void Teleport()
    {
        
    }

    public virtual void FinishTeleport()
    {
        isAnimationFinished = true;
    }
}
