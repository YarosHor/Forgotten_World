using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyGround : EnemyState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingGroundLeadge;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool rollInput;
    public EnemyGround(Enemy enemy, EnemyCurrentState currentState, EnemyDat enemyDat, string animBoolName) : base(enemy, currentState, enemyDat, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        //core.CollisionsCheck.getDud();
        isGrounded = enemy.CollisionsCheck.Ground;
        isTouchingGroundLeadge = enemy.CollisionsCheck.Leadge;
        isTouchingWall = enemy.CollisionsCheck.WallFront;
        isTouchingLedge = enemy.CollisionsCheck.LedgeHorizontal;
    }

    public override void Enter()
    {
        Debug.Log("PlayerGround");
        base.Enter();
        //player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }



    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Debug.Log("EGrounded" + isGrounded);
        Debug.Log("EGroundLeadge" + isTouchingGroundLeadge);
        Debug.Log("EWall" + isTouchingWall);
        Debug.Log("ELeadge" + isTouchingLedge);
        Debug.Log("ECeiling" + isTouchingCeiling);

       

        Debug.Log("===========");
        Debug.Log(rollInput);
        Debug.Log("===========");
        if (enemy.CollisionsCheck.PlayerFront)
        {
            currentState.ChangeState(enemy.enemyMeleeAttack);
        }
        else if (enemyData.canSee)
        {
            float distance = enemy.playerTransform.position.x - enemy.transform.position.x;
            float heigth = enemy.playerTransform.position.y - enemy.transform.position.y;
            Debug.Log("gunginginga " +heigth);
            if (heigth > -0.5 && heigth < 0.5)
            {
                if ((distance > 0f && distance < 10f && enemy.Movement.FacingDirection >= 0) || (distance < 0f && distance > -10f && enemy.Movement.FacingDirection < 0))
                {
                    if (enemyData.canRangeAttack)
                    {
                        currentState.ChangeState(enemy.enemyRangedAttack);


                    }
                    else if (enemyData.canFollow)
                    {
                        currentState.ChangeState(enemy.enemyMove);
                    }
                }
            }
            else if ((distance > 0f && enemy.Movement.FacingDirection < 0) || (distance < 0f && enemy.Movement.FacingDirection >= 0))
            {
                enemy.Movement.Flip();
                Debug.Log("dale vieja" + (enemy.playerTransform.position.x - enemy.transform.position.x));
            }


        }

        /*if (player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        if (player.InputHandler.Light1Input)
        {
            currentState.ChangeState(player.playerAttack1);
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
