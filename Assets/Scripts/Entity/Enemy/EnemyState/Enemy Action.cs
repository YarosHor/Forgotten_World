using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : EnemyState
{
    protected bool isAbilityDone;

    private bool isGrounded;
    public EnemyAction(Enemy enemy, EnemyCurrentState currentState, EnemyDat enemyData, string animBoolName) : base(enemy, currentState, enemyData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = enemy.CollisionsCheck.Ground;
    }

    public override void Enter()
    {
        Debug.Log("PlayerAction");
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        /*float distance = enemy.playerTransform.position.x - enemy.transform.position.x;
        Debug.Log("dale vieja" + distance + " greb " + enemy.Movement.FacingDirection);
        if ((distance > 0f && enemy.Movement.FacingDirection < 0) || (distance < 0f && enemy.Movement.FacingDirection > 0))
        {
            enemy.Movement.Flip();
            Debug.Log("dale vieja" + distance + " greb " + enemy.Movement.FacingDirection);
        }*/

        if (isAbilityDone)
        {
            if (enemyData.canIdle)
            {
                currentState.ChangeState(enemy.enemyIdle);
            }
            else
            {
                currentState.ChangeState(enemy.enemyMove);
            }
            /*if (isGrounded && enemy.Movement.CurrentVelocity.y < 0.01f)
            {
                currentState.ChangeState(enemy.enemyIdle);
            }
            else
            {
                currentState.ChangeState(enemy.enemyAir);
            }*/
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
