using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyLvl1_StunState : StunState
{
    private SwordEnemyLvl1 enemy;
    public SwordEnemyLvl1_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunStateData stateData, SwordEnemyLvl1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
