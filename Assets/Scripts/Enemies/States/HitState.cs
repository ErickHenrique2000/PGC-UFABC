using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    protected D_HitState stateData;

    public bool hitOver;
    public HitState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_HitState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        hitOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= stateData.hitTime + startTime)
        {
            hitOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
