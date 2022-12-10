using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    int coins = 1500;
    MoneyDisplay mp;
    public event Action<int> onMoneyChange;
    // Start is called before the first frame update
    void Start()
    {
        onMoneyChange(coins);
    }

    public void AddMoney(int addition)
    {
        coins += addition;
    }
    public bool SpendMoney(int price)
    {
        if (coins < price) return false;
        coins -= price;
        onMoneyChange(coins);
        return true; 
    }
}
