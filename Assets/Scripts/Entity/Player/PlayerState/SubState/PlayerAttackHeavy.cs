using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHeavy : PlayerAction
{
    public PlayerAttackHeavy(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }
}
