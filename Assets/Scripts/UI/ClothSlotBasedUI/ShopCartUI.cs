using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCartUI : ClothSlotBasedUI
{
    [SerializeField] private TextMeshProUGUI selectedItemPriceUIText;
    [SerializeField] private TextMeshProUGUI totalPriceUIText;
    public static ShopCartUI Instance { get; private set; }

    public override void Awake()
    {
        base.Awake();
        Instance = this;
        ShopCart.OnChangeClothesOnShopCart += UpdateSlotsUI;
        ShopCartClothSlot.OnSelectCartShopCartClothSlot += SlotSelectManager;

    }

    public override void ShowUI()
    {
        base.ShowUI();
        UpdateSlotsUI(ShopCart.Instance.clothes);
    }

    protected override void UpdateSlotsUI(List<Cloth> cloths)
    {
        
        base.UpdateSlotsUI(cloths);
        totalPriceUIText.text = "$"+ShopCart.Instance.GetTotalCartBalance().ToString("0.00");
        UpdateCurrentSelectedClothPrice();

    }

    public override void SlotSelectManager(ClothSlot selectedClothSlot)
    {
        base.SlotSelectManager(selectedClothSlot);
        int index = clothesSlots.IndexOf(selectedClothSlot);
        ShopCart.Instance.SetSelectedCloth(index);
        UpdateCurrentSelectedClothPrice();



    }

    private void UpdateCurrentSelectedClothPrice()
    {
        Cloth cloth = ShopCart.Instance.GetCurrentSelectedCloth();
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
        ShopCart.OnChangeClothesOnShopCart -= UpdateSlotsUI;
        ShopCartClothSlot.OnSelectCartShopCartClothSlot -= SlotSelectManager;
    }

}
