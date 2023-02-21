using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateMachineMonoBehaviour<PlayerState>
{
    private PlayerInput playerInput;
    private Rigidbody2D rigidBody2D;
    [SerializeField] private float speed = 10;
    public static Action<Vector2> OnReciveWalkInput;

    private void Awake()
    {
        DialogueManager.OnStartDialogue += SetTalkingState;
        

        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();

        ChangeState(PlayerState.Walking);

    }

    public override void OnExitState()
    {
        switch (currentState)
        {
            case PlayerState.Walking:

                playerInput.Disable();
                break;
            case PlayerState.Interacting:
                break;
            case PlayerState.Talking:
                DialogueManager.OnFinishDialogue += SetWalkState;
                break;
            default:

                break;
        }

    }

    public override void OnEnterState(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Walking:
                playerInput.Enable();

                break;
            case PlayerState.Interacting:

                break;

            case PlayerState.Talking:
                DialogueManager.OnFinishDialogue += SetWalkState;
                break;
            default:
                break;
        }

    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlayerState.Walking:
                Vector2 walkInputValues = playerInput.PlayerActions.Walk.ReadValue<Vector2>();
                walkInputValues = Vector2Converter.ConvertToSigleDirection(walkInputValues); // Convert input to deny diagonal movement
                OnReciveWalkInput?.Invoke(walkInputValues);
                MoveCharacter(walkInputValues);

                break;
            case PlayerState.Interacting:

                break;
            default:

                break;
        }

    }

    private void MoveCharacter(Vector2 walkInputValues)
    { 
        rigidBody2D.velocity = walkInputValues * speed * Time.deltaTime;

    }

    private void SetTalkingState()
    {
        ChangeState(PlayerState.Talking);
    }

    private void SetWalkState()
    {
        ChangeState(PlayerState.Walking);
    }

    private void OnDisable()
    {
        DialogueManager.OnStartDialogue -= SetTalkingState;
        OnExitState();
    }

}

public enum PlayerState
{
    Walking,
    Talking,
    Interacting

}



