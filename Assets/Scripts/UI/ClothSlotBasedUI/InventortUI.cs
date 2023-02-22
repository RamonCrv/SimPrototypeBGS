using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventortUI : ClothSlotBasedUI
{
    [SerializeField] private Image currentEquipedClothUI;
    public override void Awake()
    {
        base.Awake();
        PlayerInventory.OnChangeClothesOnInventory += UpdateSlotsUI;
        InventoryClothSlot.OnSelectInventoryClothSlot += SlotSelectManager;

    }

    public override void ShowUI()
    {
        base.ShowUI();
        UpdateSlotsUI(PlayerInventory.Instance.clothes);

    }

    protected override void UpdateSlotsUI(List<Cloth> cloths)
    {
        base.UpdateSlotsUI(cloths);
        UpdateEquipedClothUI();


    }

    public override void SlotSelectManager(ClothSlot selectedClothSlot)
    {
        base.SlotSelectManager(selectedClothSlot);
        int index = clothesSlots.IndexOf(selectedClothSlot);
        PlayerInventory.Instance.SetSelectedCloth(index);


    }

    private void UpdateEquipedClothUI()
    {
        Cloth cloth = PlayerInventory.Instance.GetCurrentEquipedCloth();
        if (cloth != null)
        {
            currentEquipedClothUI.enabled = true;
            currentEquipedClothUI.sprite = cloth.clothMainSprite;
        }
        else
        {
            currentEquipedClothUI.enabled = false;
        }
    }

    private void OnDisable()
    {
        PlayerInventory.OnChangeClothesOnInventory -= UpdateSlotsUI;
        InventoryClothSlot.OnSelectInventoryClothSlot -= SlotSelectManager;
    }

}
