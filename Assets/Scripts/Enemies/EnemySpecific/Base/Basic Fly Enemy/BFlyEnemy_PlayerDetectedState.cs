using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFlyEnemy_PlayerDetectedState : PlayerDetectedState
{
    private BFlyEnemy enemy;
    private Transform playerTransform;

    public BFlyEnemy_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, BFlyEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
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

        if(isPlayerInMinAgroRange)
        {
            //Debug.Log("PD -> SEEK");
            stateMachine.ChangeState(enemy.seekState);
        }else if(!isPlayerInMaxAgroRange)
        {
            //Debug.Log("PD -> IDLE");
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
