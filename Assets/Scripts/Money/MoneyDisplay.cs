using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay   : MonoBehaviour
{
    TMP_Text moneyText;
    // Start is called before the first frame update
    void OnEnable()
    {
        moneyText = GetComponentInChildren<TMP_Text>();
        MoneyManager man = FindObjectOfType<MoneyManager>();
        man.onMoneyChange += UpdateMoneyCont;
    }
    
    // Update is called once per frame
    public void UpdateMoneyCont (int newCoins)
    {
        moneyText.text = newCoins.ToString();


    }
}
