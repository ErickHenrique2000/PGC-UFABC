﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeEnemy_StunState : StunState
{
    private BasicMeleeEnemy enemy;
    public BasicMeleeEnemy_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunStateData stateData, BasicMeleeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isStunTimeOver)
        {
            if(performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }else if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }else
            {
                enemy.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
