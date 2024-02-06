using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Cloth", menuName = "ScriptableObjects/Cloth")]
public class Cloth : ScriptableObject
{
    public Sprite clothMainSprite;
    public float price;
    public SpriteSheet clothSpriteSheet;
}
