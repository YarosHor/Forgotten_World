using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerCrouch : PlayerGround
{
    public PlayerCrouch(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player.SetColliderHeight(playerData.crouchColliderHeight);
        player.Movement.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
        //player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            currentState.ChangeState(player.playerIdle);
        }
    }
}
