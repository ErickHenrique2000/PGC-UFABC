﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyLvl1_DeadState : DeadState
{
    private SwordEnemyLvl1 enemy;
    public SwordEnemyLvl1_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, SwordEnemyLvl1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
