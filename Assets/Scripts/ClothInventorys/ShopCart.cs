using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ShopCart : ClothInventory
{
    public static ShopCart Instance { get; private set; }    
    public static Action<List<Cloth>> OnChangeClothesOnShopCart;
    public static Action OnSelectNewCloth;


    private void Awake()
    {
        Instance = this;
    }

    public override void RemoveClothFromList()
    {
        base.RemoveClothFromList();
        OnChangeClothesOnShopCart?.Invoke(clothes);
    }

    public override void AddClothToList(Cloth newCloth)
    {
        base.AddClothToList(newCloth);
        OnChangeClothesOnShopCart?.Invoke(clothes);
    }

    public override void SetSelectedCloth(int index)
    {
        base.SetSelectedCloth(index);
        OnSelectNewCloth?.Invoke();
    }

    public float GetTotalCartBalance()
    {
        float totalBalance = 0;
        foreach (var cloth in clothes)
        {
            totalBalance += cloth.price;
        }

        return totalBalance;
    }

}
