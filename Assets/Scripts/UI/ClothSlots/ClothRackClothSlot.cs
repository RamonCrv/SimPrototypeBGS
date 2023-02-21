using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClothRackClothSlot : ClothSlot
{
    public static Action<ClothRackClothSlot> OnSelectClothRackClothSlot;
    public override void Select()
    {
        base.Select();
        OnSelectClothRackClothSlot?.Invoke(this);

    }
}
