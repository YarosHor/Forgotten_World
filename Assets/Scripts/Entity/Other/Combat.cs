using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour, IDamageable, IKnockbackable
{
    private bool isKnockbackActive;
    private float knockbackStartTime;

    public void LogicUpdate()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(this.transform.parent.name + " Damaged!");
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        transform.parent.gameObject.GetComponent<Entity>().Movement.SetVelocity(strength, angle, direction);
        transform.parent.gameObject.GetComponent<Entity>().Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && transform.parent.gameObject.GetComponent<Entity>().Movement.CurrentVelocity.y <= 0.01f && transform.parent.gameObject.GetComponent<Player>().CollisionsCheck.Ground)
        {
            isKnockbackActive = false;
            transform.parent.gameObject.GetComponent<Entity>().Movement.CanSetVelocity = true;
        }
    }
}