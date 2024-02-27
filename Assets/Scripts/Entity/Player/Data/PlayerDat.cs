using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerDat", menuName = "Data/Player Data/Play Data")]
public class PlayerDat : ScriptableObject
{
    [Header("Healh")]
    public int health = 100;

    [Header("Invulnerability Time")]
    public float invulnerability = 5f;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public float standColliderHeight = 1.6f;
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Roll State")]
    public float rollEndYMultiplier = 0.2f;
}
