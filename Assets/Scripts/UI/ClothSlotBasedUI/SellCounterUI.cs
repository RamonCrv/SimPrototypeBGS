using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellCounterUI : RetractableUI
{
    [SerializeField] private Image clothToSellImage;
    [SerializeField] private TextMeshProUGUI clothPriceText;

    public override void Awake()
    {
        base.Awake();
        PlayerInventory.OnChangeClothesOnInventory += UpdateclothToSellInformationUI;
        PlayerInventory.OnSelectNewCloth += UpdateOnSelectNewCloth;
    }

    public override void ShowUI()
    {
        base.ShowUI();
        UpdateclothToSellInformationUI();
        InventortUI.Instance.ShowUI();
    }
    public override void HideUI()
    {
        base.HideUI();
        if (InventortUI.Instance != null)
        {
            InventortUI.Instance.HideUI();
        }
    }
    private void UpdateclothToSellInformationUI(List<Cloth> cloths = null)
    {
        Cloth cloth = PlayerInventory.Instance.GetCurrentSelectedCloth();
        if (cloth != null)
        {
            clothToSellImage.enabled = true;
            clothToSellImage.sprite = cloth.clothMainSprite;
            clothPriceText.text = "+$" + cloth.price.ToString("0.00");
        }
        else
        {
            clothToSellImage.enabled = false;
            clothPriceText.text = "+$0.00";
        }
        
    }

    private void UpdateOnSelectNewCloth()
    {
        UpdateclothToSellInformationUI();
    }

    private void OnDisable()
    {
        PlayerInventory.OnChangeClothesOnInventory -= UpdateclothToSellInformationUI;
        PlayerInventory.OnSelectNewCloth += UpdateOnSelectNewCloth;
    }
}
