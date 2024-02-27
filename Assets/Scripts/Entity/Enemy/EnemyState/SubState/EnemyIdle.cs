using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyIdle : EnemyGround
{
    private float stopTimer ;
    public EnemyIdle(Enemy enemy, EnemyCurrentState currentState, EnemyDat enemyData, string animBoolName) : base(enemy, currentState, enemyData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }


    public override void Enter()
    {
        base.Enter();
        stopTimer = 1f;
        Debug.Log("PlayerIdle");
        enemy.xSpeed = 0f;
        enemy.Movement.SetVelocityX(enemy.xSpeed);
        
    }

    public void start()
    {
        //StartCoroutine(WaitRoutine());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("DREGUR:" + enemy.CollisionsCheck.WallFront);
        
        if (stopTimer > 0f)
        {
            stopTimer -= Time.deltaTime;
            return;
        }
        enemy.xSpeed = -1f;
        enemy.Movement.CheckIfShouldFlip((int)Random.Range(enemy.xSpeed, 2));
        currentState.ChangeState(enemy.enemyMove);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    IEnumerator WaitRoutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
