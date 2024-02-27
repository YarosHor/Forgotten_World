using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemyDat", menuName = "Data/Enemy Data/Play Data")]
public class EnemyDat : ScriptableObject
{
    [Header("Health")]
    public int health = 100;

    [Header("AttackDamage")]
    public int damage = 25;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;

    [Header("Can Idle")]
    public bool canIdle = false;

    [Header("Can Move")]
    public bool canMove = false;

    [Header("Can Get Hurt")]
    public bool canHurt = false;

    [Header("Can Die")]
    public bool canDie = false;

    [Header("Can See")]
    public bool canSee = false;

    [Header("Can Follow")]
    public bool canFollow = false;

    [Header("Can Attack")]
    public bool canAttack = false;

    [Header("Can Range Attack")]
    public bool canRangeAttack = false;


}
