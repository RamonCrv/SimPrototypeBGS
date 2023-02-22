using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothSlot : MonoBehaviour
{
    [SerializeField] private Image clothImage;
    [SerializeField] private Image selectedIcon;
    private Cloth slotCloth;

    private void Awake()
    {
        Deselect();
    }
    public virtual void Select()
    {
        selectedIcon.enabled = true;

    }

    public void Deselect()
    {
        selectedIcon.enabled = false;
    }

    public void SetCloth(Cloth cloth)
    {
        slotCloth = cloth;
        clothImage.sprite = cloth.clothMainSprite;
        clothImage.enabled = true;
    }

    public void ResetSlot()
    {
        slotCloth = null;
        clothImage.enabled = false;
        selectedIcon.enabled = false;
    }
 

    
}
