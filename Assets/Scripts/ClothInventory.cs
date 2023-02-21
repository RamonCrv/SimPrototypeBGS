using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClothInventory : MonoBehaviour
{
    [SerializeField] protected int currentSelectedClothIndex = -1;
    [SerializeField] public List<Cloth> clothes;

    public virtual void RemoveClothFromCart()
    {
        if (currentSelectedClothIndex == -1)
        {
            return;
        }

        if (currentSelectedClothIndex >= clothes.Count)
        {
            return;
        }

        clothes.RemoveAt(currentSelectedClothIndex);

    }

    public virtual void AddClothToCart(Cloth newCloth)
    {
        if (clothes.Contains(newCloth) == true)
        {
            return;
        }

        clothes.Add(newCloth);

    }

    public void ResetSelectedCloth()
    {
        currentSelectedClothIndex = -1;
    }

    public void SetSelectedCloth(int index)
    {
        currentSelectedClothIndex = index;
    }

    public Cloth GetCurrentSelectedCloth()
    {
        if (currentSelectedClothIndex == -1)
        {
            return null;
        }

        if (currentSelectedClothIndex >= clothes.Count)
        {
            return null;
        }

        return clothes[currentSelectedClothIndex];
    }
}
