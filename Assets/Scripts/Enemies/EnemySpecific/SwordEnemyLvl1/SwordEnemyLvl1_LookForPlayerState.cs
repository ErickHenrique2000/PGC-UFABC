﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyLvl1_LookForPlayerState : LookForPlayerState
{
    private SwordEnemyLvl1 enemy;
    public SwordEnemyLvl1_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, SwordEnemyLvl1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SetTurnImmediately(bool flip)
    {
        base.SetTurnImmediately(flip);
    }
}