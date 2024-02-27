using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class OffensiveWeapon : Weapon
{
    protected SOOfensiveWeaponData SOOfensiveWeaponData;
    private List<IDamageable> damageables = new List<IDamageable>();
    

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(SOOfensiveWeaponData))
        {
            SOOfensiveWeaponData = (SOOfensiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("Wrong data for the weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleAttack();
    }

    public void CheckMeleAttack()
    {
        Debug.Log("adawd" + attackCont);
        WeaponAttackDetails details = SOOfensiveWeaponData.AttackDetails[attackCont];
        /*for(int i =  0; i < damageables.Count; i++)
        {
            if(i % 2 == 0)
            {
                SoundAttack1.Play();
            }
            else
            {

            }
            damageables[i].Damage(details.damageAmount);
        }*/
        foreach (IDamageable a in damageables.ToList()) 
        {
            a.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collider)
    {
        Debug.Log("DamagableAdded");
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null)
        {
            Debug.Log("DamagableAdded2");
            damageables.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageables.Remove(damageable);
        }
    }
}
