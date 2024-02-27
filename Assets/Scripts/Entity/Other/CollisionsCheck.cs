using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionsCheck : MonoBehaviour
{
    public Transform PlayerCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(playerCheck, transform.parent.name);
        private set => playerCheck = value;
    }
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, transform.parent.name);
        private set => groundCheck = value;
    }
    public Transform LeadgeCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(leadgeCheck, transform.parent.name);
        private set => leadgeCheck = value;
    }

    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, transform.parent.name);
        private set => wallCheck = value;
    }
    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float PlayerCheckRadius { get => playerCheckRadius; set => playerCheckRadius = value; }
    public float LeadgeCheckRadius { get => leadgeCheckRadius; set => leadgeCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public float PlayerCheckDistance { get => playerAtackRadius; set => playerAtackRadius = value; }
    public LayerMask WhatIsGround { get => ground; set => ground = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform leadgeCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float playerCheckRadius;
    [SerializeField] private float leadgeCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float playerAtackRadius;

    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask player;



    public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, ground);
    }

    public bool Leadge
    {
        get => Physics2D.OverlapCircle(LeadgeCheck.position, leadgeCheckRadius, ground);
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * transform.parent.gameObject.GetComponent<Entity>().Movement.FacingDirection, wallCheckDistance, ground);
    }

    public bool PlayerLooking
    {
        get => Physics2D.Raycast(PlayerCheck.position, Vector2.right * transform.parent.gameObject.GetComponent<Entity>().Movement.FacingDirection, playerAtackRadius, player);
    }

    public bool PlayerFront
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * transform.parent.gameObject.GetComponent<Entity>().Movement.FacingDirection, playerAtackRadius, player);
    }

    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * transform.parent.gameObject.GetComponent<Entity>().Movement.FacingDirection, wallCheckDistance, ground);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -transform.parent.gameObject.GetComponent<Entity>().Movement.FacingDirection, wallCheckDistance, ground);
    }
}