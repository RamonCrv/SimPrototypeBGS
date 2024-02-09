using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.Netcode;
public class PlayerInventory : ClothInventory
{

    [SerializeField] private Cloth equipedCloth;
    public static PlayerInventory Instance { get; private set; }
    public static Action<List<Cloth>> OnChangeClothesOnInventory;
    public static Action OnSelectNewCloth;
    public static Action OnMoneyValueChange;
    public static Action<Cloth> OnChangeEquipedCloth;

    public float currentyMoney { get; private set; }   
    private float startValue = 100;
    

    private void Awake()
    {
        

    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) return;
        Instance = this;
        GainMoney(startValue);
    }

    public override void RemoveClothFromList()
    {
        if (equipedCloth == clothes[currentSelectedClothIndex]) return;

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
            //Chamar fun��o que mdua o player
            equipedCloth = cloth;
            OnChangeEquipedCloth?.Invoke(equipedCloth);
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
        if (cloth == equipedCloth) return;

        if (cloth != null)
        {
            GainMoney(cloth.price);           
            RemoveClothFromList();

        }
    }



}
