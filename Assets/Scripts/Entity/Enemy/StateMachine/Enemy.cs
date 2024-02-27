using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyCurrentState CurrentState { get; private set; }

    public EnemyAir enemyAir { get; private set; }
    public EnemyDeath enemyDeath { get; private set; }
    public EnemyHurt enemyHurt { get; private set; }
    public EnemyIdle enemyIdle { get; private set; }
    public EnemyMeleeAttack enemyMeleeAttack { get; private set; }
    public EnemyMove enemyMove { get; private set; }
    public EnemyRangedAttack enemyRangedAttack { get; private set; }


    [SerializeField]
    private EnemyDat enemyDat;
    public AnimatorOverrideController animatorOverride;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public Transform playerTransform;
    public AudioSource soundAttack;

    private Vector2 workspace;
    public bool climbLadder;
    public bool dropPlatform;
    public Collision2D platformCollider = null;
    public GameObject projectilePrefab;
    private int playerDirection;
    public float xSpeed;

    public PlayerCheck PlayerNear;

    public void Awake()
    {
        xSpeed = 0f;
        Debug.Log("PlayerAwake");
        PlayerNear = GetComponentInChildren<PlayerCheck>();
        Movement = GetComponentInChildren<Movement>();
        CollisionsCheck = GetComponentInChildren<CollisionsCheck>();
        Combat = GetComponentInChildren<Combat>();

        CurrentState = new EnemyCurrentState();

        enemyAir = new EnemyAir(this, CurrentState, enemyDat, "air");
        enemyIdle = new EnemyIdle(this, CurrentState, enemyDat, "idle");
        enemyMove = new EnemyMove(this, CurrentState, enemyDat, "move");
        enemyMeleeAttack = new EnemyMeleeAttack(this, CurrentState, enemyDat, "melee_attack");
        enemyRangedAttack = new EnemyRangedAttack(this, CurrentState, enemyDat, "ranged_attack");


    }

    private void Start()
    {
        climbLadder = false;
        playerDirection = 0;
        HealthManager.SetHealth(enemyDat.health, enemyDat.health);
        Debug.Log("PlayerStart");
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MovementCollider = GetComponent<BoxCollider2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Anim.runtimeAnimatorController = animatorOverride;
        if(enemyDat.canIdle)
        {
            CurrentState.Initialize(enemyIdle);
        }
        else
        {
            CurrentState.Initialize(enemyMove);
        }
        
        //CurrentState.Initialize(enemyMove);
        //animatorOverride = new AnimatorOverrideController(Anim.runtimeAnimatorController);
        /*Anim.runtimeAnimatorController = animatorOverride;
        animatorOverride.ApplyOverrides(animatorOverride.GetOverrides());*/
        //A.ApplyOverrides();
        Debug.Log("PlayerStart");
        
    }

    private void Update()
    {
        
        Movement.LogicUpdate();
        Combat.LogicUpdate();
        CurrentState.CurrentState.LogicUpdate();
        //RB.AddForce(new Vector2(1, 1) * 3, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        CurrentState.CurrentState.PhysicsUpdate();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            float enemyDirection = other.transform.position.x - transform.position.x;
            if (enemyDirection > 0)
            {
                playerDirection = -1;
            }
            if (enemyDirection < 0)
            {
                playerDirection = 1;
            }
            if(enemyDirection > 0)
            {
                enemyDirection = 1;
            }
            else
            {
                enemyDirection = -1;
            }
            xSpeed = 5 * (-1 * enemyDirection);
            Debug.Log("rawr" + $"{enemyDirection} {xSpeed}");
            RB.AddForce(new Vector2(xSpeed, 5) * 10, ForceMode2D.Impulse);
            HealthManager.TakeDamage(20);
            //HealthManager.TakeDamage(other.gameObject.);
            StartCoroutine(Invulnerable());
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        platformCollider = other;
    }



    private void OnTriggerExit2D(Collider2D other)
    {
    }
    private void OnCollisionExit(Collision other)
    {
        platformCollider = null;
    }


    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    public void GenerateProjectile()
    {
        Debug.Log("Heello");
        GameObject projectile = Instantiate(projectilePrefab, (transform.position + new Vector3((float)0.8 * Movement.FacingDirection, (float)-0.5,0)), transform.rotation);
        //projectile.GetComponent<Projectile>().SetDirection(new Vector2(Movement.FacingDirection, 0));
        //Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        /*if (projectileRigidbody != null)
        {
            projectileRigidbody.velocity = spawnPoint.right * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("The projectile prefab is missing a Rigidbody2D component.");
        }*/
    }

    private void AnimationTrigger()
    {
        Debug.Log("aasdasdasdasdasdasdaswd");
        CurrentState.CurrentState.AnimationTrigger();
    }

    private void AnimtionFinishTrigger() => CurrentState.CurrentState.AnimationFinishTrigger();


    public IEnumerator Invulnerable()
    {
        
        //Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < 5; ++i)
        {
            spriteRenderer.color = new Color(255, 0, 0, 255);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
        //Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    public void Attack()
    {
        soundAttack.Play();
    }
}
