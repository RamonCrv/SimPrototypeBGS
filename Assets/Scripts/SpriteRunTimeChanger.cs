using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRunTimeChanger : MonoBehaviour
{
    public static SpriteRunTimeChanger Instance { get; private set; }
    [SerializeField] private SpriteRenderer spriteRenderer;
    private SpriteSheet currentSpriteSheet;

    private void Awake()
    {
        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int index)
    {
        if (currentSpriteSheet == null)
        {
            return;
        }

        if (currentSpriteSheet.sprites.Count >= index )
        {
            return;
        }

        spriteRenderer.sprite = currentSpriteSheet.sprites[index];
    }

    public void SetCurrentSpriteSheet(SpriteSheet spriteSheet)
    {
        currentSpriteSheet = spriteSheet;
    }


}

public class SpriteSheet
{
    public List<Sprite> sprites;

}
