using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLand : PlayerGround
{
    public PlayerLand(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                currentState.ChangeState(player.playerMove);
            }
            else if (isAnimationFinished)
            {
                currentState.ChangeState(player.playerIdle);
            }
        }
    }
}
