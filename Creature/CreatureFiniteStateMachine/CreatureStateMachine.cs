using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStateMachine
{
    public CreatureState CurrentState { get; private set; }

    public void Initialize(CreatureState startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(CreatureState newState) {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
