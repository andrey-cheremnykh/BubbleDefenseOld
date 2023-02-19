using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : Tower
{
    [SerializeField] GameObject crystal;
    bool isReloaded = true;
    float fireRate = 1;
    float slowness = 0.3f;
    float slowDuration = 1;
    float damage = 1;

    [SerializeField] LineRenderer shootLine;
    [SerializeField] ParticleSystem frozenImpact;

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
        GameObject enemy = FindObjectOfType<Enemy>().gameObject;
        if (enemy == null) return;


        if (isReloaded)
        {
            StartCoroutine(ShootBasic(enemy));
        }
    }

    IEnumerator ShootBasic(GameObject enemy)
    {
        isReloaded = false;
        StartCoroutine(DrawLine(enemy.transform.position));
        enemy.GetComponent<EnemyHealth>().GetDamage(1);
        Enemy enMove = enemy.GetComponent<Enemy>();
        StartCoroutine(enMove.SlowEnemy(slowness, slowDuration));
        yield return new WaitForSeconds(1/fireRate);
        isReloaded = true;
    }

    IEnumerator DrawLine(Vector3 enemyPos)
    {
        frozenImpact.transform.position = enemyPos;
        frozenImpact.Play();
        shootLine.SetPosition(0, crystal.transform.position);
        shootLine.SetPosition(1, enemyPos);
        yield return new WaitForSeconds(0.1f);
        shootLine.SetPosition(0, Vector3.zero);
        shootLine.SetPosition(1, Vector3.zero);

    }

    


}
