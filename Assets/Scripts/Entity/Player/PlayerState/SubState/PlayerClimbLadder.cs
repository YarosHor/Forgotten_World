using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerClimbLadder : PlayerState
{

    protected int xInput;
    protected int yInput;
    protected bool climbLadder;

    public PlayerClimbLadder(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("hey");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.MovementInputX;
        yInput = player.InputHandler.MovementInputY;
        climbLadder = player.climbLadder;

        player.Movement.SetVelocityY(1);

        if (climbLadder)
        {
            player.Movement.SetVelocityX(playerData.movementVelocity * xInput);
            player.Movement.SetVelocityY((float)1.5 * yInput);
        }
        else if (!climbLadder)
        {
            currentState.ChangeState(player.playerAir);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
