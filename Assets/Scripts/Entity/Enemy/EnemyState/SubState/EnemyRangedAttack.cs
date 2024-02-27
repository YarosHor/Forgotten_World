using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangedAttack : EnemyAction
{
    public EnemyRangedAttack(Enemy player, EnemyCurrentState currentState, EnemyDat enemyData, string animBoolName) : base(player, currentState, enemyData, animBoolName)
    {
}

public override void Enter()
{
    base.Enter();
    Debug.Log("heyyou");
    enemy.Movement.SetVelocityX(0);
}

public override void Exit()
{
    base.Exit();
    Debug.Log("animExitRanged");
        //enemy.GenerateProjectile();
        //currentState.ChangeState(enemy.enemyIdle);

}
public override void LogicUpdate()
{
    base.LogicUpdate();
    enemy.xSpeed = 0f + enemy.xSpeed;
    enemy.Movement.SetVelocityX(enemy.xSpeed);
        float distance = enemy.playerTransform.position.x - enemy.transform.position.x;
        float heigth = enemy.playerTransform.position.y - enemy.transform.position.y;
        Debug.Log("dale vieja" + distance + " greb " + enemy.Movement.FacingDirection);
        if (heigth > -0.5 && heigth < 0.5)
        {
            if ((distance > 0f && enemy.Movement.FacingDirection < 0) || (distance < 0f && enemy.Movement.FacingDirection > 0))
            {
                enemy.Movement.CheckIfShouldFlip(enemy.Movement.FacingDirection);
                Debug.Log("dale vieja" + distance + " greb " + enemy.Movement.FacingDirection);
            }
        }
        /*if (distance > 10f || distance < 0f && enemy.Movement.FacingDirection > 0 || distance < -10f && distance > 0f && enemy.Movement.FacingDirection < 0)//hacerlo mas variable
        {
            currentState.ChangeState(enemy.enemyIdle);
        }*/
    }
    public override void AnimationFinishTrigger()
    {
        Debug.Log("animFinishRanged");
        enemy.GenerateProjectile();
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    
}
