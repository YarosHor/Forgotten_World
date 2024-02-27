using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbEdge : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;
    private bool isTouchingCeiling;

    private int xInput;
    private int yInput;

    public PlayerClimbEdge(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        Debug.Log("00000000");
        base.AnimationFinishTrigger();
        player.Anim.SetBool("climbEdge", false);
    }

    public override void AnimationTrigger()
    {

        Debug.Log(isHanging);
        Debug.Log("%%%%%%%%%%%%%%%%%%%%%%%%");
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void Enter()
    {
        player.SoundClimb.Play();
        Debug.Log("ledgeClimb");
        base.Enter();

        player.Movement.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (player.Movement.FacingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (player.Movement.FacingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;

    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;
        Debug.Log("$$$$$$$$$$$");
        Debug.Log(isClimbing);
        /*if (isClimbing)
        {*/
            Debug.Log("climbed");
            player.transform.position = stopPos;
            isClimbing = false;
        //}
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();

        if (isAnimationFinished)
        {
            Debug.Log("finish");
                currentState.ChangeState(player.playerIdle);
        }
        else
        {
            xInput = player.InputHandler.MovementInputX;
            yInput = player.InputHandler.MovementInputY;
            jumpInput = player.InputHandler.JumpInput;

            player.Movement.SetVelocityZero();
            player.transform.position = startPos;

            Debug.Log("€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€");
            Debug.Log(xInput == player.Movement.FacingDirection);
            Debug.Log(isHanging);
            Debug.Log(isClimbing);
            Debug.Log(stopPos);
            Debug.Log("€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€");
            /*if (xInput == core.Movement.FacingDirection && isHanging && !isClimbing)
            {*/
                Debug.Log("ledgeClimb1");
                //CheckForSpace();
                isClimbing = true;
                player.Anim.SetBool("climbLedge", true);
           /*}
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                Debug.Log("InAirState1");
                currentState.ChangeState(player.playerAir);
            }
            else if (jumpInput && !isClimbing)
            {
                Debug.Log("WallJumpState1");
                player.playerWallJump.DetermineWallJumpDirection(true);
                currentState.ChangeState(player.playerWallJump);
            }*/
        }

    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;

    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * player.Movement.FacingDirection * 0.015f), Vector2.up, playerData.standColliderHeight, player.CollisionsCheck.WhatIsGround);
        player.Anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }

    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(player.CollisionsCheck.WallCheck.position, Vector2.right * player.Movement.FacingDirection, player.CollisionsCheck.WallCheckDistance, player.CollisionsCheck.WhatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * player.Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(player.CollisionsCheck.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down, player.CollisionsCheck.LedgeCheckHorizontal.position.y - player.CollisionsCheck.WallCheck.position.y + 0.015f, player.CollisionsCheck.WhatIsGround);
        float yDist = yHit.distance;

        workspace.Set(player.CollisionsCheck.WallCheck.position.x + (xDist * player.Movement.FacingDirection), player.CollisionsCheck.LedgeCheckHorizontal.position.y - yDist);
        return workspace;
    }

}

