using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthGUI : MonoBehaviour
{
    Slider hpBar;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = GetComponent<Slider>();
    }

    void ChangeHP(float hpPercent)
    {
        hpBar.value = hpPercent;        
    }
}
