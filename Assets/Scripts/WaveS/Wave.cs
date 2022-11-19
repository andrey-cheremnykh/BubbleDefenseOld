using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Wave : ScriptableObject
{
    public GameObject[] enemyPrefabs;
    public int enemyCount = 6;
    public float timeBetween = 1.5f;
    public float healthMult = 1;
    int enemyIndex = 0;
    public GameObject SpawnNewEnemy(List<Waypoint> path)
    {
        GameObject clone = Instantiate(enemyPrefabs[enemyIndex], new Vector3(0,200,0),Quaternion.identity);
        clone.GetComponent<EnemyHealth>().enemyHealth *= healthMult;
        clone.GetComponent<Enemy>().Go(path);
        enemyIndex++;
        if (enemyIndex >= enemyPrefabs.Length) enemyIndex = 0;
        return clone;
    }
}
