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

        if (currentSpriteSheet.sprites.Count <= index)
        {

            return;
        }
        spriteRenderer.sprite = currentSpriteSheet.sprites[index];
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
    public List<Sprite> sprites;

}
