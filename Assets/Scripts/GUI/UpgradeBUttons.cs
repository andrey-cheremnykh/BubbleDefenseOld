using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButtons : MonoBehaviour
{
    [SerializeField] TMP_Text upgradeText; 
    [SerializeField] TMP_Text deleteText; 
    [SerializeField] TMP_Text upgrade4BText; 
    public void SetPrices(int upgradePrice, int SellPrice)
    {
        upgradeText.text = "-" + upgradePrice;
        deleteText.text  = $"-{SellPrice}";
             
    }

    public void SetPrices(int SellPrice)
    {
        deleteText.text = "-" + SellPrice;
    }
    public void SetPrices(int upgradePrice, int upgrade4BPrice, int SellPrice)
    {
        upgradeText.text = "-" + upgradePrice;
        upgrade4BText.text = "-" + upgrade4BPrice;
        deleteText.text  = $"-{SellPrice}";

    }
    
}
