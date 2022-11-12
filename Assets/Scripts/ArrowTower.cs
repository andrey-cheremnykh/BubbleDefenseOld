using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{
    GameObject archer;
    GameObject enemy;
    bool isReloaded = true;
    Arrow currentarrow;
    Vector3 arrowSpawnPos;
    GameObject arrowPrefab;
        
    // Start is called before the first frame update
    void Start()
    {
        archer = transform.GetChild(0).gameObject;
        currentarrow = GetComponentInChildren<Arrow>();
        arrowSpawnPos = currentarrow.transform.localPosition;
        base.Start();
    }
    protected override IEnumerator BuildTower()
    {
        archer.SetActive(false);
       yield return  StartCoroutine(base.BuildTower());
        archer.SetActive(true);
    }
    public override IEnumerator DestroyTower()
    {
        archer.SetActive(false);
        yield return StartCoroutine(base.DestroyTower());
    }
    // Update is called once per frame
    void Update()
    {
        enemy = FindEnemyToShoot();
        if (enemy == null) return;
        
        ArcherEnemyLook();
        if(isReloaded)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {

        isReloaded = false;
        currentarrow.Launch(enemy, 10);
        yield return new WaitForSeconds(1);
        GameObject clone = Instantiate(arrowPrefab);
        clone.transform.parent = transform.GetChild(0);
        clone.transform.localPosition = arrowSpawnPos;
        clone.transform.localRotation = Quaternion.Euler(0,0,0);
        currentarrow = clone.GetComponent<Arrow>(); 
        isReloaded = true;
    }
  
    void ArcherEnemyLook()
    {
        archer.transform.LookAt(enemy.transform.position);
        float degreeY = archer.transform.rotation.eulerAngles.y;
        archer.transform.rotation = Quaternion.Euler(0, degreeY, 0);
    }
}
