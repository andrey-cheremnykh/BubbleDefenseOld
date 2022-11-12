using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerState
{
    BUILDING,
    DESTROYING,
    LEVEL_1,
    LEVEL_2,
    LEVEL_3,
    LEVEL_4A,
    LEVEL_4B
}

public abstract class Tower : MonoBehaviour
{
    Waypoint builtway;
    EnemyHealth enemy;
    public TowerState state = TowerState.BUILDING;
    public float attackRadius = 20;
    protected List<GameObject> nearbyEnemies;
    [SerializeField] Mesh[] LevelMesh1;
    [SerializeField] Mesh[] LevelMesh2;
    [SerializeField] Mesh[] LevelMesh3;
    [SerializeField] Mesh[] LevelMesh4A;
    [SerializeField] Mesh[] LevelMesh4B;
    float buildTime = 5;
    MeshFilter towerMesh; 
    ParticleSystem buildVFX;
    protected void Start()
    {
        buildVFX = GetComponent<ParticleSystem>();
        towerMesh = GetComponent<MeshFilter>();
        StartCoroutine(BuildTower());
    }
    private void OnMouseUpAsButton()
    {
        if (state == TowerState.DESTROYING) return;
    }
    protected virtual IEnumerator BuildTower()
    {
        towerMesh.mesh = LevelMesh1[0];
        buildVFX.Play();
        yield return new WaitForSeconds(buildTime);
        towerMesh.mesh = LevelMesh1[1];
        state = TowerState.LEVEL_1;
        buildVFX.Stop();
    }
    List<GameObject> FindEnemiesInRadius()
    {
        List<GameObject> nearbyEnemies = new List<GameObject>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, enemies[i].transform.position);
            if(dist < attackRadius)
            {
                nearbyEnemies.Add(enemies[i].gameObject);
            }
        }
        return nearbyEnemies;
    }
    public GameObject FindEnemyToShoot()
    {
        List<GameObject> enemylist = FindEnemiesInRadius();
        if (enemylist.Count == 0) return null;
        GameObject mainEnemy = null;
        float maxDist = 0;
        for (int i = 0; i < enemylist.Count; i++)
        {
            Enemy en = enemylist[i].GetComponent<Enemy>();
            if (enemylist[i].GetComponent<EnemyHealth>().isAlive == false)
            {
                continue;
            }
            if (maxDist < en.GetPassedDist())
            {
                maxDist = en.GetPassedDist();
                mainEnemy = enemylist[i]; 
            }
        }
        return mainEnemy;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }
    public virtual IEnumerator DestroyTower()
    {
        buildVFX.Play();
        state = TowerState.DESTROYING;
        if (state == TowerState.LEVEL_1) towerMesh.mesh = LevelMesh1[0];
        if (state == TowerState.LEVEL_2) towerMesh.mesh = LevelMesh1[0];
        if (state == TowerState.LEVEL_3) towerMesh.mesh = LevelMesh1[0];
        if (state == TowerState.LEVEL_4A) towerMesh.mesh = LevelMesh1[0];
        yield return new WaitForSeconds(5);
        builtway.buildState = BuildState.EMPTY;
        Destroy(gameObject); 
    }
}
