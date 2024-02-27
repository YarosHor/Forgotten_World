using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAction
{
    public EnemyMeleeAttack(Enemy player, EnemyCurrentState currentState, EnemyDat enemyData, string animBoolName) : base(player, currentState, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("heyyou");
        //enemy.Movement.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("animExit");
        //currentState.ChangeState(enemy.enemyIdle);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.xSpeed = 0f;
        enemy.Movement.SetVelocityX(enemy.xSpeed);
    }

    public void SetWeapon(Weapon weapon)
    {
        Debug.Log("weaponSeated");
        Debug.Log(weapon);
        Debug.Log("weaponSeated");
    }

    public void SetPlayerVelocity(float velocity)
    {
    }

    public void SetPlayerCanFlip(bool value)
    {
    }

    public override void AnimationFinishTrigger()
    {
        Debug.Log("animFinish");
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
