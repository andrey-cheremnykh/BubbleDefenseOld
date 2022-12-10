using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BuildButtons : MonoBehaviour
{
    [SerializeField] TMP_Text arrowText;
    [SerializeField] TMP_Text cannonText;
    [SerializeField] TMP_Text magicText;
    // Start is called before the first frame update
    void Start()
    {
        arrowText.text = PricesForTowers.ARROW_1.ToString();
        cannonText.text = PricesForTowers.CANNON_1.ToString();
        magicText.text = PricesForTowers.MAGIC_1.ToString();
    }
}
