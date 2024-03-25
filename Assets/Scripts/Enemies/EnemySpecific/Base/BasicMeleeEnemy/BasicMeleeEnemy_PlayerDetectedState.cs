using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMeleeEnemy_PlayerDetectedState : PlayerDetectedState
{
    private BasicMeleeEnemy enemy;
    public BasicMeleeEnemy_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, BasicMeleeEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(performCloseRangeAction)
        {
            if(Time.time >= enemy.meleeNextAttack)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        else if(performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if(!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}