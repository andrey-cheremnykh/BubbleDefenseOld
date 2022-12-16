using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LosePanel : MonoBehaviour
{
    [SerializeField] TMP_Text gemText;
    void OnEnable()
    {
        EnemySpawner spawn = FindObjectOfType<EnemySpawner>();
        int gemsEarned = spawn.GetWaveCount() - 1;
        gemText.text = gemsEarned.ToString();
        int allGems = PlayerPrefs.GetInt("gems");
        PlayerPrefs.SetInt("gems", allGems + gemsEarned);
        
          
    }
}
