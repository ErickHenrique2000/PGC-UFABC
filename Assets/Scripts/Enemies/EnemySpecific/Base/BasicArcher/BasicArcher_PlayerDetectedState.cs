using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArcher_PlayerDetectedState : PlayerDetectedState
{
    private BasicArcher enemy;
    private Transform playerTransform;
    public BasicArcher_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, BasicArcher enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (playerTransform.position.x < enemy.aliveGO.transform.position.x && enemy.facingDirection == 1)
        {
            enemy.Flip();
        }
        else if (playerTransform.position.x > enemy.aliveGO.transform.position.x && enemy.facingDirection == -1)
        {
            enemy.Flip();
        }

        if (performCloseRangeAction)
        {
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown && enemy.CheckDodge())
            {
                stateMachine.ChangeState(enemy.dodgeState);
            }
            else
            {
                if (Time.time >= enemy.meleeNextAttack)
                {
                    stateMachine.ChangeState(enemy.meleeAttackState);
                }
            }
        }
        else if (performLongRangeAction)
        {
            if (Time.time >= enemy.rangedNextAttack)
            {
                stateMachine.ChangeState(enemy.rangedAttackState);
            }
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
