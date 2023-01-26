using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinPanel : MonoBehaviour
{
    [SerializeField] TMP_Text gemText;
    void OnEnable()
    {
        EnemySpawner spawn = FindObjectOfType<EnemySpawner>();
        int gemsEarned = spawn.GetWaveCount();
        gemText.text = "" + gemsEarned;

    }
    void CheckLevelComplete()
    {
        int levelComplete = PlayerPrefs.GetInt("levels-open ");
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        if (levelComplete >= levelIndex) return;
        PlayerPrefs.SetInt("levels-open", levelComplete + 1); 
    }
}
