using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemManager : MonoBehaviour
{
    [SerializeField] TMP_Text gemText;
    // Start is called before the first frame update
    void Start()
    {
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
}
