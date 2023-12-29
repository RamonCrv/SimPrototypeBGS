using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRunTimeChanger : MonoBehaviour
{
    public static SpriteRunTimeChanger Instance { get; private set; }
    [SerializeField] private SpriteSheet defaultSpriteSheet;
    [SerializeField] private SpriteSheet currentSpriteSheet;
    [SerializeField] private SpriteRenderer spriteRenderer;
    

    private void Awake()
    {
        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCurrentSpriteSheet(null);
    }

    public void ChangeSprite(int index)
    {
        if (currentSpriteSheet == null)
        {
            return;         
        }

        //if (currentSpriteSheet.sprites.Count <= index)
        //{
        //
        //    return;
        //}
        //spriteRenderer.sprite = currentSpriteSheet.sprites[index];
        Debug.Log(spriteRenderer.sprite.name);
    }

    public void SetCurrentSpriteSheet(SpriteSheet spriteSheet)
    {
        if (spriteSheet == null)
        {
            
            currentSpriteSheet = defaultSpriteSheet;
        }
        else
        {
            currentSpriteSheet = spriteSheet;
        }
        
    }


}

[System.Serializable]
public class SpriteSheet
{
    [SerializeField] private SpriteSheetAnimation upIddleSheet;
    [SerializeField] private SpriteSheetAnimation downIddleSheet;
    [SerializeField] private SpriteSheetAnimation leftIddleSheet;
    [SerializeField] private SpriteSheetAnimation rightIddleSheet;
    [SerializeField] private SpriteSheetAnimation upWalkingSheet;
    [SerializeField] private SpriteSheetAnimation downWalkingSheet;
    [SerializeField] private SpriteSheetAnimation leftWalkingSheet;
    [SerializeField] private SpriteSheetAnimation rightWalkingSheet;

    public SpriteSheetAnimation UpIddleSheetAnimation { get => upIddleSheet; }
    public SpriteSheetAnimation DownIddleSheetAnimation { get => downIddleSheet; }
    public SpriteSheetAnimation LeftIddleSheetAnimation { get => leftIddleSheet; }
    public SpriteSheetAnimation RightIddleSheetAnimation { get => rightIddleSheet; }
    public SpriteSheetAnimation UpWalkingSheetAnimation { get => upWalkingSheet; }
    public SpriteSheetAnimation DownWalkingSheetAnimation { get => downWalkingSheet; }
    public SpriteSheetAnimation LeftWalkingSheetAnimation { get => leftWalkingSheet; }
    public SpriteSheetAnimation RightWalkingSheetAnimation { get => rightWalkingSheet; }
}

[System.Serializable]
public class SpriteSheetAnimation
{
    [SerializeField] private List<Sprite> animationSheet;
    [SerializeField] private float animationSpeed = 0.166f;

    public List<Sprite> AnimationSheet { get => animationSheet;}
    public float AnimationSpeed { get => animationSpeed;}
}


