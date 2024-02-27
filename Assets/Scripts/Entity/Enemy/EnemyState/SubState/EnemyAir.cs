using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAir : EnemyState
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

    public EnemyAir(Enemy enemy, EnemyCurrentState currentState, EnemyDat enemyDat, string animBoolName) : base(enemy, currentState, enemyDat, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = enemy.CollisionsCheck.Ground;
        isTouchingWall = enemy.CollisionsCheck.WallFront;
        isTouchingWallBack = enemy.CollisionsCheck.WallBack;
        isTouchingLedge = enemy.CollisionsCheck.LedgeHorizontal;

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

        //dashInput = player.InputHandler.DashInput;


        /*if (player.InputHandler.Light1Input)
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
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            currentState.ChangeState(player.WallGrabState);
        }
        else
        {
            player.Movement.CheckIfShouldFlip(xInput);
            player.Movement.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.Movement.CurrentVelocity.x));
        }*/

    }

    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
