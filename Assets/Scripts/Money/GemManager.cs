using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    [SerializeField] TMP_Text gemText;
    [SerializeField] GameObject ExtraGemsPanel;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("gems", 1000);
        int gemsAll = PlayerPrefs.GetInt("gems");
        gemText.text = "" + gemsAll;
    }

    public bool SpendGems(int price)
    {
        int gemsAll = PlayerPrefs.GetInt("gems");
        if (price > gemsAll) return false;
        gemsAll -= price;
        PlayerPrefs.SetInt("gems", gemsAll);
        return true;
    }
    public void AddGems(int newGems)
    {
        int gemsAll = PlayerPrefs.GetInt("gems");
        PlayerPrefs.SetInt("gems", gemsAll + newGems);
    }
    public void ShowBuyGemsPanel()
    {
        ExtraGemsPanel.SetActive(true);
    }
    public void HideBuyGemsPanel()
    {
        ExtraGemsPanel.SetActive(false);
    }public void BuyPileGems()
    {
        AddGems(20);
        // take away 50c
    }public void BuyBagGems()
    {
        AddGems(50);
        // take away 1eur
    }public void BuyCartGems()
    {
        AddGems(100);
        // take away 1.5eur
    }public void BuyCastleGems()
    {
        AddGems(200);
        // take away 2eur
    }
    
    
}
