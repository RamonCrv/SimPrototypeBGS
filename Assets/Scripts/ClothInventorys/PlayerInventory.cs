using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class PlayerInventory : ClothInventory
{

    [SerializeField] private Cloth equipedCloth;
    public static PlayerInventory Instance { get; private set; }
    public static Action<List<Cloth>> OnChangeClothesOnInventory;
    public static Action OnSelectNewCloth;
    public static Action OnMoneyValueChange;

    public float currentyMoney { get; private set; }   
    private float startValue = 100;
    

    private void Awake()
    {
        Instance = this;
        GainMoney(startValue);
    }

    public override void RemoveClothFromList()
    {
        base.RemoveClothFromList();
        OnChangeClothesOnInventory?.Invoke(clothes);
    }

    public override void AddClothToList(Cloth newCloth)
    {
        base.AddClothToList(newCloth);
        OnChangeClothesOnInventory?.Invoke(clothes);
    }

    public override void SetSelectedCloth(int index)
    {
        base.SetSelectedCloth(index);
        OnSelectNewCloth?.Invoke();
    }

    public void EquipCloth()
    {
        Cloth cloth = GetCurrentSelectedCloth();
        if (cloth != null)
        {
            //Chamar função que mdua o player
            equipedCloth = cloth;
            OnChangeClothesOnInventory?.Invoke(clothes);
        }
    }

    public Cloth GetCurrentEquipedCloth()
    {
        return equipedCloth;
    }


    private void GainMoney(float gainAmout)
    {
        currentyMoney += gainAmout;
        OnMoneyValueChange?.Invoke();

    }
    private void WasteMoney(float gainAmout)
    {
        currentyMoney -= gainAmout;
        if (currentyMoney < 0)
        {
            currentyMoney = 0;
        }
        OnMoneyValueChange?.Invoke();

    }

    public bool Purchase(float price)
    {
        if (currentyMoney > price)
        {
            WasteMoney(price);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SellCloth()
    {
        Cloth cloth = GetCurrentSelectedCloth();
        if (cloth != null)
        {
            GainMoney(cloth.price);           
            if (cloth == equipedCloth)
            {
                equipedCloth = null;
            }
            RemoveClothFromList();

        }
    }



}
