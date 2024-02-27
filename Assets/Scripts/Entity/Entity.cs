using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Movement Movement;
    public CollisionsCheck CollisionsCheck;
    public Combat Combat;
    public HealthManager HealthManager;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionsCheck = GetComponentInChildren<CollisionsCheck>();
        Combat = GetComponentInChildren<Combat>();
        HealthManager = GetComponent<HealthManager>();
        //HealthManager = new HealthManager();
        
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
        Combat.LogicUpdate();
    }
}
