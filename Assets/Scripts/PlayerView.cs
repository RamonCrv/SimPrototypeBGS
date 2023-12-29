using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : StateMachineMonoBehaviour<AnimationState>
{
    private int currentAnimationSpriteIndex;   
    private SpriteRenderer spriteRenderer;
    private Coroutine changeSpriteCoroutine;
    private SpriteSheetAnimation currentSpriteSheetAnimation;
    private SpriteSheet currentClothSpriteSheet;
    [SerializeField] private AnimationState lastState;

    private void OnEnable()
    {
        PlayerInventory.OnChangeEquipedCloth += SetCurrentClothSpriteSheet;
        PlayerController.OnReciveWalkInput += DetermineWalkingSprite;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (currentClothSpriteSheet == null)
        {          
            currentClothSpriteSheet = PlayerInventory.Instance.GetCurrentEquipedCloth().clothSpriteSheet;
        }

        if (currentSpriteSheetAnimation == null)
        {
            currentSpriteSheetAnimation = currentClothSpriteSheet.DownIddleSheetAnimation;
            lastState = AnimationState.DownIdle;
        }
        changeSpriteCoroutine = StartCoroutine(ChangeSpriteTimer());
    }

   

    private void Update()
    {

        DetermineWalkingSprite(PlayerController.Instance.PlayerInput.PlayerActions.Walk.ReadValue<Vector2>());

    }


    private void DetermineWalkingSprite(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;

            // Determine the angle and set the appropriate sprite.
            if (angle > 45f && angle <= 135f)
            {
                ChangeState(AnimationState.UpWalking);
            }
            else if (angle > -45f && angle <= 45f)
            {
                ChangeState(AnimationState.RightWalking);
            }
            else if (angle > -135f && angle <= -45f)
            {
                ChangeState(AnimationState.DownWalking);
            }
            else
            {
                ChangeState(AnimationState.LeftWalking);
            }
        }
        else
        {
            switch (currentState)
            {
                case AnimationState.UpWalking:
                    ChangeState(AnimationState.UpIdle);
                    break;
                case AnimationState.DownWalking:
                    ChangeState(AnimationState.DownIdle);
                    break;
                case AnimationState.LeftWalking:
                    ChangeState(AnimationState.LeftIdle);
                    break;
                case AnimationState.RightWalking:
                    ChangeState(AnimationState.RightIdle);
                    break;
                default:
                    break;
            }
        }
            
    }

    private void SetCurrentClothSpriteSheet(Cloth newCloth)
    {
        currentClothSpriteSheet = newCloth.clothSpriteSheet;
        SetCurrentSpriteSheetAnimation(currentState);
    }

    public override void ChangeState(AnimationState newState)
    {

        if (newState == currentState) return;
        
        lastState = currentState;
       
        base.ChangeState(newState);

    }

    private void ResetSpriteCoroutine()
    {
        if (changeSpriteCoroutine != null)
        {
            StopCoroutine(changeSpriteCoroutine);
            changeSpriteCoroutine = null;
        }

        currentAnimationSpriteIndex = 0;

    }

    public override void OnEnterState(AnimationState newState)
    {
        base.OnEnterState(newState);

        SetCurrentSpriteSheetAnimation(newState);

        ResetSpriteCoroutine();
        changeSpriteCoroutine = StartCoroutine(ChangeSpriteTimer());

    }

    private void SetCurrentSpriteSheetAnimation(AnimationState state)
    {
        switch (state)
        {
            case AnimationState.UpIdle:
                currentSpriteSheetAnimation = currentClothSpriteSheet.UpIddleSheetAnimation;

                break;
            case AnimationState.DownIdle:
                currentSpriteSheetAnimation = currentClothSpriteSheet.DownIddleSheetAnimation;

                break;
            case AnimationState.LeftIdle:
                currentSpriteSheetAnimation = currentClothSpriteSheet.LeftIddleSheetAnimation;

                break;
            case AnimationState.RightIdle:
                currentSpriteSheetAnimation = currentClothSpriteSheet.RightIddleSheetAnimation;

                break;
            case AnimationState.UpWalking:
                currentSpriteSheetAnimation = currentClothSpriteSheet.UpWalkingSheetAnimation;

                break;
            case AnimationState.DownWalking:
                currentSpriteSheetAnimation = currentClothSpriteSheet.DownWalkingSheetAnimation;

                break;
            case AnimationState.LeftWalking:
                currentSpriteSheetAnimation = currentClothSpriteSheet.LeftWalkingSheetAnimation;

                break;
            case AnimationState.RightWalking:
                currentSpriteSheetAnimation = currentClothSpriteSheet.RightWalkingSheetAnimation;

                break;
            default:
                break;
        }
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

    private void SetNewSprite()
    {
        
        spriteRenderer.sprite = currentSpriteSheetAnimation.AnimationSheet[currentAnimationSpriteIndex];
    }

    private IEnumerator ChangeSpriteTimer()
    {
        SetNewSprite();
        yield return new WaitForSeconds(currentSpriteSheetAnimation.AnimationSpeed);

        if (currentAnimationSpriteIndex + 1 >= currentSpriteSheetAnimation.AnimationSheet.Count)
        {
            currentAnimationSpriteIndex = 0;
        }
        else
        {
            currentAnimationSpriteIndex++;
        }
        
        changeSpriteCoroutine = StartCoroutine(ChangeSpriteTimer());
    }

    private void OnDisable()
    {
        PlayerInventory.OnChangeEquipedCloth -= SetCurrentClothSpriteSheet;
        PlayerController.OnReciveWalkInput -= DetermineWalkingSprite;
    }


}

public enum AnimationState
{
    UpIdle,
    DownIdle,
    LeftIdle,
    RightIdle,
    UpWalking,
    DownWalking,
    LeftWalking,
    RightWalking,

}
