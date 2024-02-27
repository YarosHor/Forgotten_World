using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCurrentState : MonoBehaviour
{
    public EnemyState CurrentState { get; private set; }

    public void Initialize(EnemyState state)
    {
        Debug.Log(state.ToString());
        CurrentState = state;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyState state)
    {
        Debug.Log(state.ToString());
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
}
