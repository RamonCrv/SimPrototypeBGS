using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClothInventory : MonoBehaviour
{
    [SerializeField] protected int currentSelectedClothIndex = -1;
    [SerializeField] public List<Cloth> clothes;

    public virtual void RemoveClothFromList()
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

    public virtual void AddClothToList(Cloth newCloth)
    {
        Debug.Log(newCloth);
        if (newCloth == null)
        {
            Debug.Log("� nula");
            return;
        }
        if (clothes.Contains(newCloth) == true)
        {
            Debug.Log("J� tem");
            return;
        }
        Debug.Log("ADicionou");
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
