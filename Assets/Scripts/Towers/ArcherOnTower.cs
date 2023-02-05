using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherOnTower : MonoBehaviour
{
    GameObject enemy;
    Arrow currentarrow;
    Vector3 arrowSpawnPos;
    bool isReloaded;
    Tower t;
    private void Start()
    {
        enemy = t.FindEnemyToShoot();
        currentarrow = GetComponentInChildren<Arrow>();
        arrowSpawnPos = currentarrow.transform.localPosition;
        
    }
    private void Update()
    {
        GameObject enToShoot = t.FindEnemyToShoot();
        ArcherEnemyLook(enToShoot);
    }

    public IEnumerator Shoot(GameObject enemy, float damage, float fireRate, GameObject arrowPrefab)
    {


        isReloaded = false;
        currentarrow.Launch(enemy, 1);
        yield return new WaitForSeconds(1 / fireRate);
        GameObject clone = Instantiate(arrowPrefab);
        clone.transform.parent = transform;
        clone.transform.localPosition = arrowSpawnPos;
        clone.transform.localRotation = Quaternion.Euler(0, 0, 0);
        currentarrow = clone.GetComponent<Arrow>();
        isReloaded = true;
    }
        void ArcherEnemyLook(GameObject enemy)
        {
            enemy = GetComponent<Tower>().FindEnemyToShoot();
            transform.LookAt(enemy.transform.position);
            float degreeY = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, degreeY, 0);
        }
}
