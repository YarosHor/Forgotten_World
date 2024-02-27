using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private OffensiveWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<OffensiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("EnteredCollision");
        weapon.AddToDetected(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        weapon.RemoveFromDetected(collision);
    }
}
