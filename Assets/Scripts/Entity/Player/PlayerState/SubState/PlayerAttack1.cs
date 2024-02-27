using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAttack1 : PlayerAction
{
    private float velocityToSet;
    private bool setVelocity;
    private int xInput;
    private bool canFlip;
    private Weapon weapon;
    public PlayerAttack1(Player player, PlayerCurrentState currentState, PlayerDat playerData, string animBoolName) : base(player, currentState, playerData, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        setVelocity = false;
        canFlip = true;
        Debug.Log("weaponEnter");
        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.MovementInputX;
        if(canFlip )
        {
            player.Movement.CheckIfShouldFlip(xInput);
        }
        if (setVelocity)
        {
            player.Movement.SetVelocityX(player.Movement.FacingDirection * velocityToSet);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        Debug.Log("weaponSeated");
        Debug.Log(weapon);
        Debug.Log("weaponSeated");
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
    }

    public void SetPlayerVelocity(float velocity)
    {
        player.Movement.SetVelocityX(player.Movement.FacingDirection * velocity);
        velocityToSet = velocity;
        setVelocity = true;
    }

    public void SetPlayerCanFlip(bool value)
    {
        canFlip = value;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
