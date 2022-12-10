using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DisplayMoney (int coins)
    {
        TMP_Text mText = GetComponentInChildren<TMP_Text>();
        mText.text = "" + coins;
    }
}
