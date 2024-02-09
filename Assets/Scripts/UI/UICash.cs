using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UICash : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentMoneyUI;
    private void Awake()
    {
        PlayerInventory.OnMoneyValueChange += UpdateCurrentMoneyUI;
        UpdateCurrentMoneyUI();
    }

    private void UpdateCurrentMoneyUI()
    {
        //currentMoneyUI.text = "$" + PlayerInventory.Instance.currentyMoney.ToString("0.00");
    }

    private void OnDisable()
    {
        PlayerInventory.OnMoneyValueChange -= UpdateCurrentMoneyUI;
    }
}
