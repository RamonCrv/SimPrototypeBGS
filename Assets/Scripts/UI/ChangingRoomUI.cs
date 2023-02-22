using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingRoomUI : RetractableUI
{
    [SerializeField] private Image clothImage;

    public override void Awake()
    {
        base.Awake();
        ShopCart.OnSelectNewCloth += UpdateClothImage;
        ShopCart.OnChangeClothesOnShopCart += UpdateUIOnChangeShopCart;
    }

    public override void ShowUI()
    {
        base.ShowUI();
        ResetClothImage();
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

    private void UpdateClothImage()
    {
        Cloth cloth = ShopCart.Instance.GetCurrentSelectedCloth();
        if (cloth != null)
        {
            clothImage.enabled = true;
            clothImage.sprite = cloth.clothMainSprite;
        }
        else
        {
            ResetClothImage();
        }
        
    }

    private void UpdateUIOnChangeShopCart(List<Cloth> clothes)
    {
        UpdateClothImage();
    }

    private void ResetClothImage()
    {
        clothImage.enabled = false;
    }

    private void OnDisable()
    {
        ShopCart.OnSelectNewCloth -= UpdateClothImage;
        ShopCart.OnChangeClothesOnShopCart -= UpdateUIOnChangeShopCart;
    }


}
