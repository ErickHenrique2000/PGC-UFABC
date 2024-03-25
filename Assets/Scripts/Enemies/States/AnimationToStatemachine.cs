using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
    public AttackState attackState;
    public DeadState deadState;
    public TeleportState teleportState;
    private void TriggerDeath()
    {
        deadState.AfterDeathAnim();
    }

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }

    private void TriggerTeleport()
    {
        teleportState.Teleport();
    }

    private void FinishTeleport()
    {
        teleportState.FinishTeleport();
    }
}
