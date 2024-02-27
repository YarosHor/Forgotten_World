using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newOfensiveWeaponData", menuName = "Data/Weapon Data/Ofensive Weapon")]
public class SOOfensiveWeaponData : SOMyWeaponData
{
    [SerializeField] public WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;
        movementSpeed = new float[amountOfAttacks];

        for(int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}
