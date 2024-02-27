using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerAction
{
    private int jumpCount;

    public PlayerJump(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
        jumpCount = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.SoundJump.Play();
        player.InputHandler.UseJumpInput();
        player.Movement.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        doJump();
        player.playerAir.SetIsJumping();
    }


    public bool canJump()
    {
        if (jumpCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void resetJumps() => jumpCount = playerData.amountOfJumps;

    public void doJump()
    {
        jumpCount--;
    }
}
