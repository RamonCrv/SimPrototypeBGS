using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerController.OnReciveWalkInput += SetMovementParameters;
    }
    
    public void SetMovementParameters(Vector2 inputValues)
    {
        if (inputValues == Vector2.zero)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        animator.SetBool("IsWalking", true);
        animator.SetFloat("HorizontalInput", inputValues.x);
        animator.SetFloat("VerticalInput", inputValues.y);
    }

    private void OnDisable()
    {
        PlayerController.OnReciveWalkInput -= SetMovementParameters;
    }
}
