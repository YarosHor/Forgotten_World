using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SOMyWeaponData weaponData;
    [SerializeField] protected AnimatorOverrideController animation;
    [SerializeField] protected Sprite sprite;
    public AudioSource SoundAttack1, SoundAttack2;

    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerAttack1 state;


    protected int attackCont;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Body").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        weaponAnimator.runtimeAnimatorController = animation as RuntimeAnimatorController;
        gameObject.SetActive(false);
    }

    public void SetWeapon(GameObject weapon)
    {
        weaponAnimator.runtimeAnimatorController = weapon.GetComponent<Change_Weapon>().skin1 as RuntimeAnimatorController;
        AnimatorOverrideController oldActualSkin = weapon.GetComponent<Change_Weapon>().skin1;
        Sprite oldActualSprite = weapon.GetComponent<SpriteRenderer>().sprite;
        SOMyWeaponData oldWeaponData = weapon.GetComponent<Change_Weapon>().weaponData;
        weapon.GetComponent<Change_Weapon>().ChangeValues(sprite, animation, weaponData, transform.position);
        weaponData = oldWeaponData;
        sprite = oldActualSprite;
        animation = oldActualSkin;

    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        //if(attackCont >= weaponData.movementSpeed.Length)
        if (attackCont >= weaponData.amountOfAttacks)
        {
            attackCont = 0;
        }

        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);

        baseAnimator.SetInteger("attackCont", attackCont);
        weaponAnimator.SetInteger("attackCont", attackCont);
        if (attackCont % 2 == 0)
        {
            SoundAttack1.Play();
        }
        else
        {
            SoundAttack2.Play();
        }
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);

        attackCont++;

        gameObject.SetActive(false);
    }


    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCont]);
        state.SetPlayerCanFlip(false);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
        state.SetPlayerCanFlip(true);
    }

    public virtual void AnimationActionTrigger() { }

    public void InitializeWeapon(PlayerAttack1 state/*, Core core*/)
    {
        this.state = state;
        //this.core = core;
    }

}
