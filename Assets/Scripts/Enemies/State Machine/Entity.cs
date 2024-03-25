using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform dodgeLedgeCheck;
    [SerializeField]
    private Transform dodgeWallCheck;

    public int currentHealth;

    public int lastDamageDirection { get; private set; }

    private float currentStunResistance;
    private float lastDamageTime;

    protected bool isStunned;
    public bool isPatrol, hasWideVision;
    public bool isDead;

    private Vector2 velocityWorkSpace;
    private Vector3 respawnPos;

    public virtual void Start()
    {
        respawnPos = transform.position;
        this.gameObject.layer = entityData.layer;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;
        facingDirection = 1;
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", rb.velocity.y);

        if(Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        if(hasWideVision)
        {
            return Physics2D.OverlapBox(playerCheck.position, entityData.minVisionSize, entityData.visionAngle, entityData.whatIsPlayer);
        }else
        {
            return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
        }
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        if(hasWideVision)
        {
            return Physics2D.OverlapBox(playerCheck.position, entityData.maxVisionSize, entityData.visionAngle, entityData.whatIsPlayer);
        }else
        {
            return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
        }
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }
    public virtual bool CheckDodge()
    {
        bool canDodge = Physics2D.OverlapCircle(dodgeLedgeCheck.position, entityData.groundCheckRadius, entityData.whatIsGround) &&
            !Physics2D.OverlapCircle(dodgeWallCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
        return canDodge;
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkSpace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkSpace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);

        if(attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }else
        {
            lastDamageDirection = 1;
        }

        if(currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if(currentHealth <=0)
        {
            isDead = true;
        }
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void Respawn()
    {
        isDead = false;
        aliveGO.SetActive(true);
        aliveGO.layer = 10;
        currentHealth = entityData.maxHealth;
        aliveGO.transform.position = respawnPos;
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        if(isPatrol)
        {
            if (playerTransform.position.x < aliveGO.transform.position.x && facingDirection == -1)
            {
                Flip();
            }
            else if (playerTransform.position.x > aliveGO.transform.position.x && facingDirection == 1)
            {
                Flip();
            }
        }else
        {
            if (playerTransform.position.x < aliveGO.transform.position.x && facingDirection == 1)
            {
                Flip();
            }
            else if (playerTransform.position.x > aliveGO.transform.position.x && facingDirection == -1)
            {
                Flip();
            }
        }
    }
    public virtual void OnDrawGizmos()
    {
        Vector2 wireCubeSize = new Vector2(0.1f, 0.1f);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        if(entityData.canDodge)
        {
            Gizmos.DrawWireCube(dodgeWallCheck.position, wireCubeSize);
            Gizmos.DrawWireCube(dodgeLedgeCheck.position, wireCubeSize);
        }

        if(hasWideVision)
        {
            Gizmos.DrawWireCube(playerCheck.position, entityData.minVisionSize);
            Gizmos.DrawWireCube(playerCheck.position, entityData.maxVisionSize);
        }
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance), 0.3f);
    }
}
