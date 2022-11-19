using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawnBar : MonoBehaviour
{
    float startX , endX = 0;
    float startWidth, endWidth;
    float startHeight;
    float maxX;
    float maxWidth;
    float fillSpeed = 0;
    RectTransform fill;
    float timer = 0;
    TMP_Text waveText;

    // Start is called before the first frame update
    void Start()
    {   
        fill = transform.GetChild(0).GetComponent<RectTransform>();
        waveText = GetComponentInChildren<TMP_Text>();
        maxWidth = fill.rect.width;
        WaveSpawnBar sawnBar = FindObjectOfType<WaveSpawnBar>();
        maxX = fill.transform.localPosition.x;
        startHeight = fill.rect.height; 
        startX = fill.localPosition.x;
        startWidth = fill.rect.height;
        SetWaveFill(20,4);
    }
    private void Update()
    {
        timer += Time.deltaTime * fillSpeed;
        float newWidth = Mathf.Lerp(startWidth,endWidth,timer);
        fill.sizeDelta = new Vector2(newWidth, startHeight);
        float newX = Mathf.Lerp(startX, endX, timer);
        float y = fill.transform.localPosition.y;
        fill.transform.localPosition = new Vector3(newX, y, 0);
    }
    public void SetWaveFill(float timeWave, int waveCount)
    {
        startWidth = 0;
        endWidth = maxWidth;
        startX = maxX - maxWidth / 2;
        endX = maxX;
        timer = 0;
        fillSpeed = 1 / timeWave;
        waveText.text = $"Wave :{waveCount}";
        GetComponent<Button>().interactable = false;
    }
    public void StartBuildTime(float buildTime)
    {
        startWidth = maxWidth;
        endWidth = 0;
        startX = maxX;
        endX = maxX - maxWidth / 2;
        timer = 0;
        fillSpeed = 1 / buildTime;
        waveText.text = "Build Time";
        GetComponent<Button>().interactable = false;
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
        fill.sizeDelta = new Vector2(startWidth, startHeight);
        while(timer < 1)
        {
            yield return null;
            timer += Time.deltaTime / duration;
        }
    }
}
