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
    }

    private void UpdateCurrentMoneyUI(float money)
    {
        currentMoneyUI.text = "$" + money.ToString("0.00");
    }

    private void OnDisable()
    {
        PlayerInventory.OnMoneyValueChange -= UpdateCurrentMoneyUI;
    }
}
