using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelButton[] buttons = GetComponentsInChildren<LevelButton>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetButtonParams(i + 1);
        }
    }

}
