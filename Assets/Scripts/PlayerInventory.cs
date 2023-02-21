using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : ClothInventory
{
    public static PlayerInventory Instance { get; private set; }
    public float currentyMoney { get; private set; }
    public static Action<float> OnMoneyValueChange;
    public static Action OnFinishPurchase;
    private float startValue = 100;  

    private void Awake()
    {
        Instance = this;
        GainMoney(startValue);
    }
    private void GainMoney(float gainAmout)
    {
        currentyMoney += gainAmout;
        OnMoneyValueChange?.Invoke(currentyMoney);

    }
    private void WasteMoney(float gainAmout)
    {
        currentyMoney -= gainAmout;
        if (currentyMoney < 0)
        {
            currentyMoney = 0;
        }
        OnMoneyValueChange?.Invoke(currentyMoney);

    }

    public bool Purchase(float price)
    {
        if (currentyMoney > price)
        {
            WasteMoney(price);
            OnFinishPurchase?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }



}
