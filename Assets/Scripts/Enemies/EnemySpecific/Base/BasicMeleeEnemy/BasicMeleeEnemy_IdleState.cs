﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeEnemy_IdleState : IdleState
{
    private BasicMeleeEnemy enemy;
    public BasicMeleeEnemy_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, BasicMeleeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if(isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
