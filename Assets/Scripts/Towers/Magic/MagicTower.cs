using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : Tower
{
    [SerializeField] GameObject crystal;
    float fireRate = 1;
    bool isReloaded = true;
    float slowness = 0.3f;
    float damage = 1;
    float slowDuration = 1;
    [SerializeField] LineRenderer line;
    [SerializeField] ParticleSystem FreezeImpact;
    protected override IEnumerator BuildTower()
    {
        crystal.SetActive(false);
        yield return StartCoroutine(base.BuildTower());
        crystal.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        if (state == TowerState.BUILDING || state == TowerState.DESTROYING) return;
        GameObject en = FindEnemyToShoot();
        if (en == null) return;
        if(isReloaded == true)
        {
            StartCoroutine(ShootBasic(en));
        }
    }
    IEnumerator ShootBasic(GameObject enemy)
    {
        isReloaded = false; 
        StartCoroutine(DrawLine(enemy.transform.position)); 
        enemy.GetComponent<EnemyHealth>().GetDamage(1);
        Enemy enMove = enemy.GetComponent<Enemy>();
        StartCoroutine(enMove.GetComponent<Enemy>().SlowEnemy(slowness,slowDuration));
        yield return new WaitForSeconds(1 / fireRate);
        isReloaded = true;

    }
    IEnumerator DrawLine(Vector3 enemyPos)
    {
        FreezeImpact.transform.position = enemyPos;
        FreezeImpact.Play();
        line.SetPosition(0, crystal.transform.position);
        line.SetPosition(1, enemyPos);
        yield return new WaitForSeconds(0.1f); 
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, enemyPos);

    }
}
