using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawnBar : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;
    [SerializeField] RectTransform fill;
    float startX;
    float startWidth;
    float height;
    // Start is called before the first frame update
    void Start()
    {
        WaveSpawnBar sawnBar = FindObjectOfType<WaveSpawnBar>();
        startX = fill.localPosition.x;
        startWidth = fill.rect.height;
    }
    void BeginNewWave()
    {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(spawner.SpawnNewWave());
    }
    public void SetText(string s)
    {
        waveText.text = s;
    }
    public IEnumerator Setbuild(float duration)
    {
        float timer = 0;
        SetText("Build Time");
        fill.sizeDelta = new Vector2(startWidth, height);
        while(timer < 1)
        {
            yield return null;
            timer += Time.deltaTime / duration;
        }
    }
}
