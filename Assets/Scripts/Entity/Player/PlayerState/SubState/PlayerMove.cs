using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerGround
{
    public PlayerMove(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
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
        
        player.Movement.CheckIfShouldFlip(xInput);

        player.Movement.SetVelocityX(playerData.movementVelocity * xInput);

        if (!isExitingState)
        {
            if (xInput == 0)
            {
                currentState.ChangeState(player.playerIdle);
            }
            else if (yInput == -1)
            {
                //currentState.ChangeState(player.playerCrouch);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
