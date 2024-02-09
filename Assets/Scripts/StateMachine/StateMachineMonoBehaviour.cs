using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StateMachineMonoBehaviour<TState>: NetworkBehaviour
{
    [SerializeField] public NetworkVariable<TState> currentState = new NetworkVariable<TState>((TState)Enum.GetValues(typeof(TState)).GetValue(0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public virtual void ChangeState(TState newState)
    {
        if (!IsOwner) return;
        OnExitState();
        currentState.Value = newState;
        OnEnterState(newState);
    }

    public virtual void OnExitState() //Executes exit comands of the current state
    {
        

    }

    public virtual void OnEnterState(TState newState) //Executes enter comands of the new state
    {


    }

    [ServerRpc]
    protected void TestServerRpc(string Message)
    {
        Debug.Log("Client(" + OwnerClientId + "): " + Message);
    }

    
}
