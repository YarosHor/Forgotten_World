using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemyGround
{
    private int direction;
    private float stopTimer;
    public EnemyMove(Enemy enemy, EnemyCurrentState currentState, EnemyDat enemyDat, string animBoolName) : base(enemy, currentState, enemyDat, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        stopTimer = 3f;
        direction = enemy.Movement.FacingDirection;
        Debug.Log("hey");
        stopTimer = (float)Random.Range(1, 5);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("GRAGUR:" + enemy.CollisionsCheck.PlayerFront);
        Debug.Log(enemy.CollisionsCheck.WallFront);
        enemy.Movement.CheckIfShouldFlip(direction);
        Debug.Log("jola" + enemy.CollisionsCheck.PlayerFront);
        //Debug.Log("jallo" + enemy.CollisionsCheck.PlayerLooking);
        
        if (enemy.CollisionsCheck.WallFront || !enemy.CollisionsCheck.Leadge)
        {
            direction = direction * -1;
        }
        enemy.xSpeed = enemyData.movementVelocity;
        enemy.Movement.SetVelocityX(enemy.xSpeed * direction);
        /*if(enemyData.canFollow && !enemyData.canRangeAttack && enemy.PlayerNear.seesPlayer == true)
        {

        }*/
        if (false) { }
        else{
            if (stopTimer > 0f)
            {
                stopTimer -= Time.deltaTime;
                return;
            }
            if (enemyData.canIdle)
            {
                currentState.ChangeState(enemy.enemyIdle);
            }
        }
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
