using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StateMachineMonoBehaviour<TState>: NetworkBehaviour, IStateMachine<TState> where TState : Enum
{
    public TState currentState { get; set; }

    public virtual void ChangeState(TState newState)
    {
        
        OnExitState();
        currentState = newState;
        OnEnterState(newState);
    }

    public virtual void OnExitState() //Executes exit comands of the current state
    {
        

    }

    public virtual void OnEnterState(TState newState) //Executes enter comands of the new state
    {
        

    }
}
