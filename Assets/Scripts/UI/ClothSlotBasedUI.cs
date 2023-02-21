using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ClothSlotBasedUI : MonoBehaviour
{
    [SerializeField] protected List<ClothSlot> clothesSlots;
    [SerializeField] protected Vector2 openPosition;
    [SerializeField] protected Vector2 closePosition;
    [SerializeField] protected Vector2 openScale;

    protected float animationTime = 0.2f;
    protected bool isOpen = true;
    protected bool isAnimating = false;

    private CanvasGroup canvasGroup;

    public virtual void Awake()
    {
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

    protected virtual void ShowUI()
    {
        isOpen = true;
        AnimatePanel(openPosition, openScale, 1);
    }

    protected void HideUI()
    {
        isOpen = false;
        AnimatePanel(closePosition, Vector2.zero, 0);
    }

    protected void AnimatePanel(Vector2 position, Vector2 scale, float alpha)
    {
        LeanTween.moveLocal(gameObject, position, animationTime);
        LeanTween.scale(gameObject, scale, animationTime);
        LeanTween.alphaCanvas(canvasGroup, alpha, animationTime);
    }

    protected IEnumerator AnimationCouter()
    {
        isAnimating = true;
        yield return new WaitForSeconds(animationTime);
        isAnimating = false;
    }

    protected virtual void UpdateSlotsUI(List<Cloth> cloths)
    {
        Debug.Log("Entrou no Update base. Quantidade de roupas: "+cloths.Count);
        for (int i = 0; i < clothesSlots.Count; i++)
        {
            if (i < cloths.Count) //For each cloth in the list, give them to a slot
            {
                Debug.Log(cloths[i]);
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

        //int index = clothSlots.IndexOf(shopCartClothSlot);
        //ShopCart.Instance.SetSelectedCloth(index);
        //
        //Cloth cloth = ShopCart.Instance.GetCurrentSelectedCloth();
        //if (cloth != null)
        //{
        //    selectedItemPriceUIText.text = "$" + cloth.price;
        //}
        //else
        //{
        //    selectedItemPriceUIText.text = "$0.00";
        //}

    }



  
}
