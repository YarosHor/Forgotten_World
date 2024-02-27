using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAir : PlayerState
{
    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool dashInput;

    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;

    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    public PlayerAir(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CollisionsCheck.Ground;
        isTouchingWall = player.CollisionsCheck.WallFront;
        isTouchingWallBack = player.CollisionsCheck.WallBack;
        isTouchingLedge = player.CollisionsCheck.LedgeHorizontal;

        if (isTouchingWall && !isTouchingLedge)
        {
            player.playerClimbEdge.SetDetectedPosition(player.transform.position);
        }

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        Debug.Log("PlayerAir");
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.MovementInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        //dashInput = player.InputHandler.DashInput;

        CheckJumpMultiplier();

        /*if (player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            currentState.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            currentState.ChangeState(player.SecondaryAttackState);
        }*/
        /*Debug.Log(isGrounded);
        Debug.Log(core.Movement.CurrentVelocity.y);*/
        if (player.InputHandler.Light1Input)
        {
            currentState.ChangeState(player.playerAttack1);
        }
        else if (player.InputHandler.Light2Input)
        {
            currentState.ChangeState(player.playerAttack2);
        }
        else if (player.InputHandler.Heavy1Input)
        {
            currentState.ChangeState(player.playerAttackHeavy);
        }
        else if (isGrounded && player.Movement.CurrentVelocity.y < 0.01f)
        //else if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            currentState.ChangeState(player.playerLand);
        }
        else if (player.climbLadder)
        {
            currentState.ChangeState(player.playerClimbLadder);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = player.CollisionsCheck.WallFront;
            player.playerWallJump.DetermineWallJumpDirection(isTouchingWall);
            currentState.ChangeState(player.playerWallJump);
        }
        else if (jumpInput && player.playerJump.canJump())
        {
            currentState.ChangeState(player.playerJump);
        }
        /*else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            currentState.ChangeState(player.WallGrabState);
        }*/
        else if (isTouchingWall && !isTouchingLedge)
        {
            Debug.Log("4");
            currentState.ChangeState(player.playerClimbEdge);
        }
        /*else if (isTouchingWall && xInput == player.Movement.FacingDirection && player.Movement.CurrentVelocity.y <= 0)
        {
            currentState.ChangeState(player.playerWallSlide);
        }*/
        /*else if (dashInput && player.DashState.CheckIfCanDash())
        {
            currentState.ChangeState(player.DashState);
        }*/
        else
        {
            player.Movement.CheckIfShouldFlip(xInput);
            player.Movement.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.Movement.CurrentVelocity.x));
        }

    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.Movement.SetVelocityY(player.Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.Movement.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.playerJump.doJump();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    public void SetIsJumping() => isJumping = true;
}
