using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CashierUI : RetractableUI
{
    [SerializeField] private TextMeshProUGUI totalBalanceText;

    public override void Awake()
    {
        base.Awake();
        ShopCart.OnChangeClothesOnShopCart += UpdateTotalBalance;
    }

    public override void ShowUI()
    {
        base.ShowUI();
        UpdateTotalBalance();
        ShopCartUI.Instance.ShowUI();
    }
    public override void HideUI()
    {
        base.HideUI();
        if (ShopCartUI.Instance != null)
        {
            ShopCartUI.Instance.HideUI();
        }
    }
    private void UpdateTotalBalance(List<Cloth> cloths = null)
    {
        float totalBalance = ShopCart.Instance.GetTotalCartBalance();
        totalBalanceText.text = "$" + totalBalance.ToString("0.00");
    }

    private void OnDisable()
    {
        ShopCart.OnChangeClothesOnShopCart -= UpdateTotalBalance;
    }
}
