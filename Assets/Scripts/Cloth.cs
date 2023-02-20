using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cloth", menuName = "ScriptableObjects/Cloth")]
public class Cloth : ScriptableObject
{
    public Sprite clothMainSprite;
    public float price;
    public Sprite clothSpriteSheet;
}
