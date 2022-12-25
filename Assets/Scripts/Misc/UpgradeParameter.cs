using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeParameter : MonoBehaviour
{
    [SerializeField] Text priceText;
    [SerializeField] Text valueText;

    [SerializeField] string parameterID;

    [Header("Levels Parameters")]
    [SerializeField] int[] prices;
    [SerializeField] int[] values;


    // Start is called before the first frame update
    void Start()
    {
        DisplayParameters();
    }


    void DisplayParameters()
    {
        int levelNumber = PlayerPrefs.GetInt(parameterID);
        priceText.text = ""+ prices[levelNumber];
        valueText.text = "" + values[levelNumber];
    }

    public void UpgradeParameterButton()
    {
        int levelNumber = PlayerPrefs.GetInt(parameterID);
        if (levelNumber >= prices.Length) return;
        GemManager gm = FindObjectOfType<GemManager>();
        bool isBought = gm.SpendGems(prices[levelNumber]);
        if(isBought)
        {
            PlayerPrefs.SetInt(parameterID, levelNumber + 1);
            DisplayParameters();
        }
    }
   
}
