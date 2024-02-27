using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerHurt : PlayerAction
{
    public PlayerHurt(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        //core.CollisionsCheck.getDud();
    }

    public override void Enter()
    {
        Debug.Log("PlayerHurt");
        base.Enter();
        player.hurt = true;
        //player.DashState.ResetCanDash();
        Debug.Log("Ignore");
        //player.StartCoroutine(player.Invulnerable());
    }
    
    

    public override void Exit()
    {
        base.Exit();
        player.hurt = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishTrigger()
    {
        Debug.Log("animFinish");
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
