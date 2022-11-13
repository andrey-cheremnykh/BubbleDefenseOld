using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SpawnState
{
    SPAWNING,
    NON_SPAWNING,
    WIN,
    LOSE
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    int waveCount = 0;

    Pathfinder pathfinder;
    WaveSpawnBar spawnBar;

    float timer = 0;
    public SpawnState state = SpawnState.NON_SPAWNING;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnNewWave());
        }

        if (waveCount > 0 && state == SpawnState.NON_SPAWNING)
        {
            timer += Time.deltaTime;

            if (timer > 20)
            {
                timer = 0;
                StartCoroutine(SpawnNewWave());
            }

        }

    }

    public IEnumerator SpawnNewWave()
    {
        if (state != SpawnState.NON_SPAWNING) yield break;
        timer = 0;
        state = SpawnState.SPAWNING;
        spawnBar.SetText("WAve:" + (waveCount + 1));
        int enemiesCount = waves[waveCount].enemyCount;
        float timeBetween = waves[waveCount].timeBetween;

        List<Waypoint> path = pathfinder.FindPath();
        Waypoint spawnPoint = GetComponentInChildren<Waypoint>();
        path.Insert(0, spawnPoint);

        for (int i = 0; i < enemiesCount; i++)
        {
            waves[waveCount].SpawnNewEnemy(path);
            yield return new WaitForSeconds(timeBetween);
        }

        waveCount++;
        StartCoroutine(SpawnNewWave());
    }

    IEnumerator CheckEnemiesAlive()
    {
        yield return new WaitForSeconds(1);
        int aliveEnemies = 0;
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].isAlive == true) aliveEnemies++;
        }
        if (aliveEnemies > 0) StartCoroutine(CheckEnemiesAlive());
        else CheckWin();

    }
    void CheckWin()
    {
        if(waveCount == waves.Length)
        {
            state = SpawnState.WIN;
            
        }
        else
        {
            state -= SpawnState.SPAWNING;
        }
    }


}