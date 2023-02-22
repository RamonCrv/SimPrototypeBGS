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
    public static PlayerController Instance { get; private set; }

    public static Action OnEnterWalkingState;
    public static Action OnExitWalkingState;

    private void Awake()
    {
        DialogueManager.OnStartDialogue += SetTalkingState;

        Instance = this;
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();

        ChangeState(PlayerState.Walking);

    }

    public override void OnExitState()
    {
        switch (currentState)
        {
            case PlayerState.Walking:
                OnExitWalkingState?.Invoke();
                playerInput.Disable();

                break;
            case PlayerState.Interacting:
                break;
            case PlayerState.Talking:
                DialogueManager.OnFinishDialogue += SetWalkingState;
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
                OnEnterWalkingState?.Invoke();

                break;
            case PlayerState.Interacting:

                break;

            case PlayerState.Talking:
                DialogueManager.OnFinishDialogue += SetWalkingState;
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

    public void SetTalkingState()
    {
        ChangeState(PlayerState.Talking);
    }

    public void SetWalkingState()
    {
        ChangeState(PlayerState.Walking);
    }

    public void SetInteractingState()
    {
        ChangeState(PlayerState.Interacting);
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



