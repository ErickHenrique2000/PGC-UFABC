using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BFlyEnemy_SeekState : SeekState
{
    private BFlyEnemy   enemy;
    private Transform playerTransform;
    public BFlyEnemy_SeekState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SeekState stateData, Seeker seeker, BFlyEnemy enemy) : base(entity, stateMachine, animBoolName, stateData, seeker)
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
        target = playerTransform;
        seeker.StartPath(enemy.rb.position, target.position, OnPathComplete);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (target.position.x < enemy.aliveGO.transform.position.x && enemy.facingDirection == 1)
        {
            enemy.Flip();
            seeker.StartPath(enemy.rb.position, target.position, OnPathComplete);
        }
        else if (target.position.x > enemy.aliveGO.transform.position.x && enemy.facingDirection == -1)
        {
            enemy.Flip();
            seeker.StartPath(enemy.rb.position, target.position, OnPathComplete);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (path == null)
        {
            reachedEndOfPath = true;
            stateMachine.ChangeState(enemy.playerDetectedState);
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            if(isPlayerInMaxAgroRange)
            {
                seeker.StartPath(enemy.rb.position, target.position, OnPathComplete);
            }else
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
        }
        else
        {
            reachedEndOfPath = false;
        }

/*        if(!isPlayerInMaxAgroRange && Time.time - startTime >= 10f)
        {
            target = enemy.transform;
            seeker.StartPath(enemy.rb.position, target.position, OnPathComplete);
        }else if(isPlayerInMaxAgroRange && target != playerTransform)
        {
            target = playerTransform;
            seeker.StartPath(enemy.rb.position, target.position, OnPathComplete);
        }*/

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemy.rb.position).normalized;
        Vector2 force = direction * stateData.speed * Time.deltaTime;

        float distance = Vector2.Distance(enemy.rb.position, path.vectorPath[currentWaypoint]);

        enemy.rb.AddForce(force);

        if (distance < stateData.nextWayPointDistance)
        {
            currentWaypoint++;
        }
    }

    public void OnPathComplete(Path path)
    {
        if(!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }
}
