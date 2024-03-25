using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SeekState : State
{
    protected D_SeekState stateData;

    protected Seeker seeker;
    protected Path path;
    protected Transform target;

    protected bool reachedEndOfPath;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    protected int currentWaypoint;

    public SeekState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SeekState stateData, Seeker seeker) : base(entity, stateMachine, animBoolName)
    {
        this.seeker = seeker;
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = entity.CheckLedge();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        currentWaypoint = 0;
        reachedEndOfPath = false;
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
}
