using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ShopCart : MonoBehaviour
{
    public static ShopCart Instance { get; private set; }    
    public static Action OnChangeClothsOnShopCart;
    [SerializeField] private int currentSelectedClothIndex = -1;
    [SerializeField] public List<Cloth> cloths;

    private void Awake()
    {
        Instance = this;
    }

    public void RemoveClothFromCart()
    {
        if (currentSelectedClothIndex == -1)
        {
            return;
        }

        cloths.RemoveAt(currentSelectedClothIndex);
        OnChangeClothsOnShopCart?.Invoke();
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

        return cloths[currentSelectedClothIndex];
    }

    public float GetTotalCartBalance()
    {
        float totalBalance = 0;
        foreach (var cloth in cloths)
        {
            totalBalance += cloth.price;
        }

        return totalBalance;
    }

}
