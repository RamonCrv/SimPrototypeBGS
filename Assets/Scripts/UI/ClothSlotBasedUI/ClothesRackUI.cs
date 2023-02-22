using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClothesRackUI : ClothSlotBasedUI
{
    [SerializeField] private TextMeshProUGUI selectedItemPriceUIText;
    public override void Awake()
    {
        base.Awake();
        ClothesRack.OnChangeClothesOnShopCart += UpdateSlotsUI;
        ShopCartClothSlot.OnSelectCartShopCartClothSlot += SlotSelectManager;
        ClothRackClothSlot.OnSelectClothRackClothSlot += SlotSelectManager;

    }

    public override void ShowUI()
    {
        base.ShowUI();
        ShopCartUI.Instance.ShowUI();
        UpdateSelectedItemPriceUI();
        UpdateSlotsUI(ClothesRack.Instance.clothes);
    }

    public override void HideUI()
    {
        base.HideUI();
        if (ShopCartUI.Instance != null)
        {
            ShopCartUI.Instance.HideUI();
        }
        
    }

    public override void SlotSelectManager(ClothSlot selectedClothSlot)
    {
        base.SlotSelectManager(selectedClothSlot);
        int index = clothesSlots.IndexOf(selectedClothSlot);
        ClothesRack.Instance.SetSelectedCloth(index);
        UpdateSelectedItemPriceUI();


    }

    private void UpdateSelectedItemPriceUI()
    {
        Cloth cloth = ClothesRack.Instance.GetCurrentSelectedCloth();
        if (cloth != null)
        {
            selectedItemPriceUIText.text = "$" + cloth.price.ToString("0.00");
        }
        else
        {
            selectedItemPriceUIText.text = "$0.00";
        }
    }

    private void OnDisable()
    {
        ClothesRack.OnChangeClothesOnShopCart -= UpdateSlotsUI;
        ClothRackClothSlot.OnSelectClothRackClothSlot -= SlotSelectManager;

    }

}
