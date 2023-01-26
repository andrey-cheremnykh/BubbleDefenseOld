using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDefenders : MonoBehaviour
{
    [SerializeField] GameObject archerPrefab;
    [SerializeField] GameObject arrowPrefab;
    int archerCount = 0;
    ArcherOnTower[] archers;
    bool isShooting;
    float fireRate = 1;
    float damage = 6;
    // Start is called before the first frame update
    void Start()
    {
        archerCount = 3;
        SpawnArchers();
    }
    int GetArcherCount()
    {
        int level = PlayerPrefs.GetInt("archers");
        return level;
    }
    void SpawnArchers()
    {
        archerCount = PlayerPrefs.GetInt("archers");
        archers = new ArcherOnTower[archerCount];
        for (int i = 0; i < archerCount; i++)
        {
            GameObject clone = Instantiate(archerPrefab, transform.GetChild((i)));
            clone.transform.localRotation = Quaternion.Euler(0, -90, 0);
            archers[i] = clone.GetComponent<ArcherOnTower>();
        }
    }
    GameObject FindEnemyNearCastle()
    {
        AttackPoint[] points = FindObjectsOfType<AttackPoint>();
        GameObject enemy = null;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].enemyOnPoint)
            {
                EnemyHealth enemyH = points[i].enemyOnPoint.GetComponent<EnemyHealth>();
                if (enemyH.isAlive == false) continue;
                return (points[i].enemyOnPoint);
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (isShooting) return;
        GameObject enemy = FindEnemyNearCastle();
        StartCoroutine(ShootEnemy(enemy));
    }
    IEnumerator ShootEnemy(GameObject enemy)
    {

        isShooting = true;
        for (int i = 0; i < archerCount; i++)
        {
            StartCoroutine(archers[i].Shoot(enemy,damage,fireRate,arrowPrefab));
            archers[i].transform.LookAt(enemy.transform);
            float yDegree = archers[i].transform.rotation.eulerAngles.y;
            archers[i].transform.rotation = Quaternion.Euler(0, yDegree, 0);
        }
        yield return new WaitForSeconds(1 / fireRate);
        isShooting = false;
    }
}
