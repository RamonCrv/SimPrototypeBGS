using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClothesRack : ClothInventory
{
    public static ClothesRack Instance { get; private set; }
    public static Action<List<Cloth>> OnChangeClothesOnShopCart;

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
    
    public void AddClothToCart()
    {
        Cloth cloth = GetCurrentSelectedCloth();
        Debug.Log(cloth);
        ShopCart.Instance.AddClothToList(GetCurrentSelectedCloth());
    }
}
