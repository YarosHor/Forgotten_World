using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWallSlide : PlayerState
{
    protected int xInput;
    protected int yInput;
    protected bool jumpInput;
    protected bool isGrounded;
    protected bool isTouchingLedge;
    protected bool isTouchingWall;

    public PlayerWallSlide(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CollisionsCheck.Ground;
        isTouchingWall = player.CollisionsCheck.WallFront;
        isTouchingLedge = player.CollisionsCheck.LedgeHorizontal;

        if (isTouchingWall && !isTouchingLedge)
        {
            player.playerClimbEdge.SetDetectedPosition(player.transform.position);
        }
    }

    public override void Enter()
    {
        Debug.Log("WallSlide");
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }




    public override void LogicUpdate()
    {
        xInput = player.InputHandler.MovementInputX;
        yInput = player.InputHandler.MovementInputY;
        jumpInput = player.InputHandler.JumpInput;
        /*isGrounded = core.CollisionsCheck.Ground;
        isTouchingLedge = core.CollisionsCheck.LedgeHorizontal;
        isTouchingWall = core.CollisionsCheck.WallFront;*/

        /*Debug.Log("*******************************");
        Debug.Log(jumpInput);
        Debug.Log(isGrounded);
        Debug.Log(isTouchingWall);
        Debug.Log(isTouchingLedge);
        Debug.Log("*******************************");*/

        base.LogicUpdate();

        if (!isExitingState)
        {
            Debug.Log("isDoing");
            player.Movement.SetVelocityY(-playerData.wallSlideVelocity);
        }


        if (jumpInput)
        {
            Debug.Log("1");
            player.playerWallJump.DetermineWallJumpDirection(isTouchingWall);
            currentState.ChangeState(player.playerWallJump);
        }
        else if (isGrounded)
        {
            Debug.Log("2");
            currentState.ChangeState(player.playerIdle);
        }
        else if (!isTouchingWall || (xInput != player.Movement.FacingDirection))
        {
            Debug.Log("3");
            currentState.ChangeState(player.playerAir);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            Debug.Log("4");
            currentState.ChangeState(player.playerClimbEdge);
        }

    }
}
