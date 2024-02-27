using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class PlayerGround : PlayerState
{

    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool rollInput;


    public PlayerGround(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        //core.CollisionsCheck.getDud();
        isGrounded = player.CollisionsCheck.Ground;
        isTouchingWall = player.CollisionsCheck.WallFront;
        isTouchingLedge = player.CollisionsCheck.LedgeHorizontal;
    }

    public override void Enter()
    {
        Debug.Log("PlayerGround");
        base.Enter();

        player.playerJump.resetJumps();
        //player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Debug.Log("Grounded" + isGrounded);
        Debug.Log("Wall" + isTouchingWall);
        Debug.Log("Leadge" + isTouchingLedge);
        Debug.Log("Ceiling" + isTouchingCeiling);

        xInput = player.InputHandler.MovementInputX;
        yInput = player.InputHandler.MovementInputY;
        JumpInput = player.InputHandler.JumpInput;
        rollInput = player.InputHandler.RollInput;

        Debug.Log("===========");
        Debug.Log(rollInput);
        Debug.Log("===========");

        /*if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }*/
        if (player.InputHandler.Light1Input)
        {
            currentState.ChangeState(player.playerAttack1);
        }
        else if (player.InputHandler.Light2Input)
        {
            currentState.ChangeState(player.playerAttack1);
        }
        else if (player.InputHandler.Heavy1Input)
        {
            currentState.ChangeState(player.playerAttackHeavy);
        }
        else if (JumpInput && player.playerJump.canJump())
        {
            currentState.ChangeState(player.playerJump);
        }
        else if (player.climbLadder)
        {
            currentState.ChangeState(player.playerClimbLadder);
        }
        else if (!isGrounded)
        {
            player.playerAir.StartCoyoteTime();
            currentState.ChangeState(player.playerAir);
        }
        else if (rollInput)
        {
            currentState.ChangeState(player.playerRoll);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
