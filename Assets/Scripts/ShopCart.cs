using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ShopCart : ClothInventory
{
    public static ShopCart Instance { get; private set; }    
    public static Action<List<Cloth>> OnChangeClothesOnShopCart;


    private void Awake()
    {
        Instance = this;
    }

    public override void RemoveClothFromCart()
    {
        base.RemoveClothFromCart();
        OnChangeClothesOnShopCart?.Invoke(clothes);
    }

    public override void AddClothToCart(Cloth newCloth)
    {
        base.AddClothToCart(newCloth);
        OnChangeClothesOnShopCart?.Invoke(clothes);
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
