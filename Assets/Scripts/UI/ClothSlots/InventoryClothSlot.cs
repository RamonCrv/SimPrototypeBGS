using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryClothSlot : ClothSlot
{
    public static Action<InventoryClothSlot> OnSelectInventoryClothSlot;
    public override void Select()
    {
        base.Select();
        OnSelectInventoryClothSlot?.Invoke(this);

    }
}
