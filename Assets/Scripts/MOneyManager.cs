using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOneyManager : MonoBehaviour
{
    int coins = 20;
    MoneyPanel mp;
    // Start is called before the first frame update
    void Start()
    {
        mp = FindObjectOfType<MoneyPanel>();
            mp.DisplayMoney(coins);
    }

    public void AddMoney(int addition)
    {
        coins += addition;
    }
    public bool SpendMoney(int price)
    {
        if (coins < price) return false;
        coins -= price;
        return true; 
    }
}
