using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCartUI : MonoBehaviour
{
    [SerializeField] private List<ClothSlot> clothSlots;
    [SerializeField] private TextMeshProUGUI selectedItemPriceUIText;
    [SerializeField] private TextMeshProUGUI totalPriceUIText;

    private Vector2 openPosition = new Vector2(700, -100);
    private Vector2 closePosition = new Vector2(915, -360);
    private Vector2 openScale = new Vector2(1.2f, 1.2f);

    private float animationTime = 0.2f;
    private bool isOpen = true;
    private bool isAnimating = false;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        ShopCart.OnChangeClothsOnShopCart += UpdateSlotsUI;
        ShopCartClothSlot.OnSelectCartShopCartClothSlot += SlotSelectManager;
        canvasGroup = GetComponent<CanvasGroup>();
        HideUI();

    }

    public void ShowOrCloseUI()
    {
        if (isAnimating == true)
        {
            return;

        }
        
        StartCoroutine(AnimationCouter());
        LeanTween.cancel(gameObject);
        if (isOpen == true)
        {
            HideUI();
        }
        else
        {           
            ShowUI();

        }
    }

    private void ShowUI()
    {
        isOpen = true;
        UpdateSlotsUI();
        AnimatePanel(openPosition, openScale, 1);
    }

    private void HideUI()
    {
        isOpen = false;
        AnimatePanel(closePosition, Vector2.zero, 0);
    }

    private void AnimatePanel(Vector2 position, Vector2 scale, float alpha)
    {
        LeanTween.moveLocal(gameObject, position, animationTime);
        LeanTween.scale(gameObject, scale, animationTime);
        LeanTween.alphaCanvas(canvasGroup, alpha, animationTime);
    }

    private IEnumerator AnimationCouter()
    {
        isAnimating = true;
        yield return new WaitForSeconds(animationTime);
        isAnimating = false;
    }

    public void UpdateSlotsUI()
    {
       

        totalPriceUIText.text = ShopCart.Instance.GetTotalCartBalance().ToString();

        List<Cloth> cloths = ShopCart.Instance.cloths;
        for (int i = 0; i < clothSlots.Count; i++)
        {
            if (i < cloths.Count) //For each cloth in the list, give them to a slot
            {
                clothSlots[i].SetCloth(cloths[i]);
            }
            else
            {
                clothSlots[i].ResetSlot();
            }
        }
    }

    public void SlotSelectManager(ClothSlot shopCartClothSlot)
    {      
        foreach (var clothSlot in clothSlots)
        {
            if (clothSlot == shopCartClothSlot)
            {
                continue;
            }

            clothSlot.Deselect();
        }

        int index = clothSlots.IndexOf(shopCartClothSlot);
        ShopCart.Instance.SetSelectedCloth(index);

        Cloth cloth = ShopCart.Instance.GetCurrentSelectedCloth();
        if (cloth != null)
        {
            selectedItemPriceUIText.text = "$" + cloth.price;
        }
        else
        {
            selectedItemPriceUIText.text = "$0.00";
        }

    }



    private void OnDisable()
    {
        ShopCart.OnChangeClothsOnShopCart -= UpdateSlotsUI;
        ShopCartClothSlot.OnSelectCartShopCartClothSlot -= SlotSelectManager;
    }

}
