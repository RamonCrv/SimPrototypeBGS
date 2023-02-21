using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ClothSlotBasedUI : RetractableUI
{
    [SerializeField] protected List<ClothSlot> clothesSlots;   

    protected virtual void UpdateSlotsUI(List<Cloth> cloths)
    {
        for (int i = 0; i < clothesSlots.Count; i++)
        {
            if (i < cloths.Count) //For each cloth in the list, give them to a slot
            {

                clothesSlots[i].SetCloth(cloths[i]);
            }
            else
            {
                clothesSlots[i].ResetSlot();
            }
        }

        
    }

    public virtual void SlotSelectManager(ClothSlot selectedClothSlot)
    {
        foreach (var clothSlot in clothesSlots)
        {
            if (clothSlot == selectedClothSlot)
            {
                continue;
            }

            clothSlot.Deselect();
        }

    }



  
}
