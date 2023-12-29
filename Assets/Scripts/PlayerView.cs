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

    private void OnEnable()
    {
        PlayerInventory.OnChangeEquipedCloth += SetCurrentClothSpriteSheet;

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
        }
        changeSpriteCoroutine = StartCoroutine(ChangeSpriteTimer());
    }

   

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ChangeState(AnimationState.UpWalking);
        }

        if (Input.GetKey(KeyCode.S))
        {
            ChangeState(AnimationState.DownWalking);
        }

        if (Input.GetKey(KeyCode.A))
        {
            ChangeState(AnimationState.LeftWalking);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ChangeState(AnimationState.RightWalking);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            ChangeState(AnimationState.UpIdle);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            ChangeState(AnimationState.DownIdle);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            ChangeState(AnimationState.LeftIdle);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            ChangeState(AnimationState.RightIdle);
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
