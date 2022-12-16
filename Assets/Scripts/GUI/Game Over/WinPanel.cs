using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
}
