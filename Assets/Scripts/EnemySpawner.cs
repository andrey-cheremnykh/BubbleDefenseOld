using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    int waveCount = 0;
    float timeBetweenWaves;
    Pathfinder pathfinder;
    float timer = 0;
    bool isSpawning;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        StartCoroutine(SpawnNewWave());
    }
    private void Update()
    {
        if (waveCount > 0 && isSpawning == false)
        {
            timer += Time.deltaTime;
            if (timer > 20)
            {
                timer = 0;
                StartCoroutine(SpawnNewWave());

            }
        }
    }

    IEnumerator SpawnNewWave()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        int enemyCount = waveCount;
        float timeBetween = waves[waveCount].enemyCount;
        List<Waypoint> path = pathfinder.FindPath();
        Waypoint spawnPoint = GetComponentInChildren<Waypoint>();
        path.Insert(0, spawnPoint);
        for (int i = 0; i < enemyCount; i++)
        {
            waves[waveCount].SpawnNewEnemy(path);
            yield return new WaitForSeconds(timeBetween);
        }
        waveCount++;
        if (waveCount < waves.Length)
        {
            StartCoroutine(SpawnNewWave());
        }
        isSpawning = false;
    }
}
