using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentState
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState state)
    {
        Debug.Log(state.ToString());
        CurrentState = state;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState state)
    {
        Debug.Log(state.ToString());
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
}
