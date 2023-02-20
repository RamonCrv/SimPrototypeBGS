using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopCartClothSlot : ClothSlot
{
    public static Action<ShopCartClothSlot> OnSelectCartShopCartClothSlot;
    public override void Select()
    {
        base.Select();
        OnSelectCartShopCartClothSlot?.Invoke(this);

    }
}
