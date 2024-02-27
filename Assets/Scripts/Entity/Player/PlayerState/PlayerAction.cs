using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : PlayerState
{
    protected bool isAbilityDone;

    private bool isGrounded;
    public PlayerAction(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CollisionsCheck.Ground;
    }

    public override void Enter()
    {
        Debug.Log("PlayerAction");
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAbilityDone)
        {
            if (isGrounded && player.Movement.CurrentVelocity.y < 0.01f)
            {
                currentState.ChangeState(player.playerIdle);
            }
            else
            {
                currentState.ChangeState(player.playerAir);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
