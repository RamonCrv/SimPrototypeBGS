using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine<TState> where TState : Enum
{
    TState currentState { get; set; }
    void ChangeState(TState state) { }     
    void OnExitState() { } //Executes exit comands of the current state
    void OnEnterState(TState state) { } //Executes Enter comands of the new state

}


