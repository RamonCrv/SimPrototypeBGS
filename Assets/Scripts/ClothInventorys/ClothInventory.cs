using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class ClothInventory : NetworkBehaviour
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
        if (newCloth == null)
        {
            return;
        }
        if (clothes.Contains(newCloth) == true)
        {
            return;
        }
        clothes.Add(newCloth);

    }

    public void AddClothesToList(List<Cloth> newClothes)
    {
        foreach (var newCloth in newClothes)
        {
            if (newCloth == null)
            {
                return;
            }

            clothes.Add(newCloth);
        }
       

    }

    public void ResetSelectedCloth()
    {
        currentSelectedClothIndex = -1;
    }

    public virtual void SetSelectedCloth(int index)
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

    public void ClearList()
    {
        clothes.Clear();
    }
}
