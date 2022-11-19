using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{
    GameObject archer;
    GameObject enemy;
    GameObject arrowPrefab;
    bool isReloaded = true;
    Arrow currentarrow;
    Vector3 arrowSpawnPos;
    float firerate = 1;
    float damagearrow = 10;
        
    // Start is called before the first frame update
    void Start()
    {
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
        ArcherOnTower archerScript = GetComponent<ArcherOnTower>();
        yield return StartCoroutine(archerScript.Shoot(enemy,damagearrow,firerate,arrowPrefab));
        isReloaded = true;
    }
  
    void ArcherEnemyLook()
    {
        archer.transform.LookAt(enemy.transform.position);
        float degreeY = archer.transform.rotation.eulerAngles.y;
        archer.transform.rotation = Quaternion.Euler(0, degreeY, 0);
    }
}
