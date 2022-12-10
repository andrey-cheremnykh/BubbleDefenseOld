using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherOnTower : MonoBehaviour
{
    Arrow currentarrow;
    Vector3 arrowSpawnPos;
    bool isReloaded;

    private void Start()
    {
        currentarrow = GetComponentInChildren<Arrow>();
        arrowSpawnPos = currentarrow.transform.localPosition;
        
    }
   
    public IEnumerator Shoot(GameObject enemy, float damage, float fireRate, GameObject arrowPrefab)
    {


        isReloaded = false;
        currentarrow.Launch(enemy, 10);
        yield return new WaitForSeconds(1 / fireRate);
        GameObject clone = Instantiate(arrowPrefab);
        clone.transform.parent = transform.GetChild(0);
        clone.transform.localPosition = arrowSpawnPos;
        clone.transform.localRotation = Quaternion.Euler(0, 0, 0);
        currentarrow = clone.GetComponent<Arrow>();
        isReloaded = true;
    }
}
