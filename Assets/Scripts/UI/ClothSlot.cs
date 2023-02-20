using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothSlot : MonoBehaviour
{
    [SerializeField] private Image clothImage;
    [SerializeField] private Image selectedIcon;

    public void Select()
    {
        selectedIcon.enabled = true;
    }

    public void Deselect()
    {
        selectedIcon.enabled = false;
    }

    public void SetCloth(Cloth cloth)
    {
        if (cloth == null)
        {

            
            
        }
    }



}
