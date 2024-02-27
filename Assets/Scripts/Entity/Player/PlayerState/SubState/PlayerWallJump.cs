using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : PlayerAction
{
    private int wallJumpDirection;

    public PlayerWallJump(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.playerJump.resetJumps();
        player.Movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.Movement.CheckIfShouldFlip(wallJumpDirection);
        player.playerJump.doJump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", player.Movement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.Movement.CurrentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -player.Movement.FacingDirection;
        }
        else
        {
            wallJumpDirection = player.Movement.FacingDirection;
        }
    }
}
