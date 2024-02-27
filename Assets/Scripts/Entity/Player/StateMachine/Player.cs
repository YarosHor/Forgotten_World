using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Player : Entity
{
    /*public Movement Movement;
    public CollisionsCheck CollisionsCheck;
    public Combat Combat;*/



    public PlayerCurrentState CurrentState { get; private set; }

    public PlayerAir playerAir { get; private set; }
    public PlayerAttack1 playerAttack1 { get; private set; }
    public PlayerAttack1 playerAttack2 { get; private set; }
    public PlayerAttackHeavy playerAttackHeavy { get; private set; }
    public PlayerClimbEdge playerClimbEdge { get; private set; }
    public PlayerClimbLadder playerClimbLadder { get; private set; }
    public PlayerCrouch playerCrouch { get; private set; }
    public PlayerHeal playerHeal { get; private set; }
    public PlayerIdle playerIdle { get; private set; }
    public PlayerInteract playerInteract { get; private set; }
    public PlayerJump playerJump { get; private set; }
    public PlayerJumpDown playerJumpDown { get; private set; }
    public PlayerLand playerLand { get; private set; }
    public PlayerLandHard playerLandHard { get; private set; }
    public PlayerMove playerMove { get; private set; }
    public PlayerRoll playerRoll { get; private set; }
    public PlayerUse playerUse { get; private set; }
    public PlayerWallJump playerWallJump { get; private set; }
    
    public PlayerHurt playerHurt { get; private set; }
    public PlayerDie playerDie { get; private set; }

    [SerializeField]
    private PlayerDat playerData;
    //public Core2 Core { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInput InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }
    public PlayerInventory Inventory { get; private set; }

    private Vector2 workspace;
    public bool hurt;
    public float enemyDirection;
    public bool climbLadder;
    public bool dropPlatform;
    public Collision2D platformCollider = null;
    public AnimatorOverrideController actualSkin;
    public Sprite actualSprite;
    public AudioSource SoundHurt, SoundDeath, SoundWalking, SoundJump, SoundChangeWeapon, SoundClimb;



    public void Awake()
    {
        
        Debug.Log("PlayerAwake");
        Movement = GetComponentInChildren<Movement>();
        CollisionsCheck = GetComponentInChildren<CollisionsCheck>();
        Combat = GetComponentInChildren<Combat>();

        CurrentState = new PlayerCurrentState();

        playerAir = new PlayerAir(this, CurrentState, playerData, "air");
        playerAttack1 = new PlayerAttack1(this, CurrentState, playerData, "attack");
        playerAttack2 = new PlayerAttack1(this, CurrentState, playerData, "attack");
        playerAttackHeavy = new PlayerAttackHeavy(this, CurrentState, playerData, "");
        //playerAttack2 = new PlayerAir(this, CurrentState, playerData, "air");
        playerClimbEdge = new PlayerClimbEdge(this, CurrentState, playerData, "climbEdge");
    playerClimbLadder = new PlayerClimbLadder(this, CurrentState, playerData, "climbLadder");
    //playerCrouch = new PlayerCrouch(this, CurrentState, playerData, "crouch");
    //playerDropPlatform = new PlayerDropPlatform(this, CurrentState, playerData, "air");
        //playerHeal = new PlayerHeal(this, CurrentState, playerData, "heal");
        playerIdle = new PlayerIdle(this, CurrentState, playerData, "idle");
        playerInteract = new PlayerInteract(this, CurrentState, playerData, "use");
        playerJump = new PlayerJump(this, CurrentState, playerData, "air");
        playerLand = new PlayerLand(this, CurrentState, playerData, "land");
        //playerLandHard = new PlayerLandHard(this, CurrentState, playerData, "land");
        playerMove = new PlayerMove(this, CurrentState, playerData, "move");
        playerRoll = new PlayerRoll(this, CurrentState, playerData, "roll");
        //playerUse = new PlayerUse(this, CurrentState, playerData, "use");
        playerWallJump = new PlayerWallJump(this, CurrentState, playerData, "air");
        //playerWallSlide = new PlayerWallSlide(this, CurrentState, playerData, "wallSlide");
        playerHurt = new PlayerHurt(this, CurrentState, playerData, "hurt");
        playerDie = new PlayerDie(this, CurrentState, playerData, "die");

    }

    private void Start()
    {
        climbLadder = false;
        hurt = false;
        HealthManager.SetHealth(playerData.health, playerData.health);

        Debug.Log("PlayerStart");
        spriteRenderer = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInput>();
        RB = GetComponent<Rigidbody2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MovementCollider = GetComponent<BoxCollider2D>();
        Inventory = GetComponent<PlayerInventory>();
        for(int i = 0; i < Inventory.weapons.Length; i++)
        {
            Debug.Log("Weapon: " + i);
            Debug.Log(Inventory.weapons[i]);
        }
        Debug.Log("End: ");
        //Debug.Log((int)CombatInputs.primary);
        //playerAttack1.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        playerAttack1.SetWeapon(Inventory.weapons[0]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        GetComponent<Animator>().runtimeAnimatorController = actualSkin as RuntimeAnimatorController;
        CurrentState.Initialize(playerIdle);
    }

    private void Update()
    {
        Movement.LogicUpdate();
        Combat.LogicUpdate();
        CurrentState.CurrentState.LogicUpdate();
        if (hurt)
        {
            //TODO
            HealthManager.TakeDamage(20);
            StartCoroutine(Invulnerable());
            hurt = false;
        }
        Debug.DrawLine(transform.position, Vector2.right * transform.gameObject.GetComponent<Entity>().Movement.FacingDirection);
        if (platformCollider != null)
        {
            if (platformCollider.gameObject.CompareTag("Platform") && InputHandler.MovementInputY == -1)
            {

                Debug.Log("platformMovement");
                dropPlatform = true;
                Physics2D.IgnoreCollision(platformCollider.collider, platformCollider.otherCollider, dropPlatform);
                StartCoroutine(ReturnStatePlatform(platformCollider));
            }
        }
    }

    private void FixedUpdate()
    {
        CurrentState.CurrentState.PhysicsUpdate();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("ladderMoment");
            climbLadder = true;
        }
        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyRanged") || other.gameObject.CompareTag("Trap"))
        {
            int direction = 0;
            float enemyDirection = other.transform.position.x - transform.position.x;
            if(enemyDirection > 0)
            {
                direction = -1;
            }
            if (enemyDirection < 0)
            {
                direction = 1;
            }
            RB.AddForce(new Vector2(direction, 1) * 3, ForceMode2D.Impulse);
            Debug.Log("EnemyAttack");
            CurrentState.ChangeState(playerHurt);
        }
        /*else if (other.gameObject.CompareTag("Trap"))
        {
            Debug.Log("EnemyRanged");
            CurrentState.ChangeState(playerHurt);
        }*/
        /*else if (other.gameObject.CompareTag("EnemyRanged"))
        {

        }*/
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        platformCollider = other;
    }

    public IEnumerator ReturnStatePlatform(Collision2D other)
    {
        dropPlatform = false;
        Debug.Log("4444444444444444444444444444");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("platformMovementFalse1");
        Debug.Log("4444444444444444444444444444");
        Physics2D.IgnoreCollision(other.collider, other.otherCollider, dropPlatform);
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("exitLadderMoment");
            climbLadder = false;
        }

        /*if (other.gameObject.CompareTag("Platform"))
        {
            Debug.Log("platformMovementFalse2");
            dropPlatform = false;
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), GetComponent<Collider2D>(), dropPlatform);
        }*/
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
    private void AnimationTrigger()
    {
        Debug.Log("aasdasdasdasdasdasdaswd");
        CurrentState.CurrentState.AnimationTrigger();
    }

    private void AnimtionFinishTrigger() => CurrentState.CurrentState.AnimationFinishTrigger();

    public IEnumerator Invulnerable()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for(int i = 0; i < playerData.invulnerability;  ++i)
        {
            spriteRenderer.color = new Color(255, 0, 0, 255);
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }


    public void ChangeSkin(GameObject skin)
    {
        SoundChangeWeapon.Play();
        GetComponent<Animator>().runtimeAnimatorController = skin.GetComponent<Change_Skin>().skin1 as RuntimeAnimatorController;
        AnimatorOverrideController oldActualSkin = skin.GetComponent<Change_Skin>().skin1;
        Sprite oldActualSprite = skin.GetComponent<SpriteRenderer>().sprite;
        skin.GetComponent<Change_Skin>().ChangeValues(actualSprite, actualSkin, transform.position);
        actualSprite = oldActualSprite;
        actualSkin = oldActualSkin;
        //Instantiate(actualSkin, (new Vector3(transform.position.x, transform.position.y + 1, 0)), transform.rotation);
        //actualSkin = skin;
    }

    public void Step()
    {
        SoundWalking.Play();
    }

}
