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
    public TowerState state = TowerState.BUILDING;
    Waypoint builtWaypoint;

    public float attackRadius = 20;

    protected float buildTime = 5;

    [SerializeField] protected Mesh[] levelMesh1;
    [SerializeField] protected Mesh[] levelMesh2;
    [SerializeField] protected Mesh[] levelMesh3;
    [SerializeField] protected Mesh[] levelMesh4A;
    [SerializeField] protected Mesh[] levelMesh4B;

    protected MeshFilter towerMesh;
    ParticleSystem buildingVFX;

    public void Start()
    {
        buildingVFX = GetComponentInChildren<ParticleSystem>();
        towerMesh = GetComponent<MeshFilter>();
        StartCoroutine(BuildTower());
    }

    virtual protected IEnumerator BuildTower()
    {
        buildingVFX.Play();
        towerMesh.mesh = levelMesh1[0];
        yield return new WaitForSeconds(buildTime);
        towerMesh.mesh = levelMesh1[1];
        state = TowerState.LEVEL_1;
        buildingVFX.Stop();
    }


    public void SetBuildWaypoint(Waypoint w)
    {
        builtWaypoint = w;
    }

   public  List<GameObject> FindEnemiesInRadius()
    {
        List<GameObject> nearEnemies = new List<GameObject>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if(distance < attackRadius)
            {
                nearEnemies.Add(enemies[i].gameObject);
            }
        }
        return nearEnemies;
    }

    public GameObject FindEnemyToShoot()
    {
        List<GameObject> enemiesList = FindEnemiesInRadius();
        if (enemiesList.Count == 0) return null;
        GameObject mainEnemy = null;
        float maxDistance = 0;

        for (int i = 0; i < enemiesList.Count; i++)
        {
            Enemy en = enemiesList[i].GetComponent<Enemy>();
            if (enemiesList[i].GetComponent<EnemyHealth>().isAlive == true)
            {
                if (maxDistance < en.GetPassedDist())
                {
                    maxDistance = en.GetPassedDist();
                    mainEnemy = enemiesList[i];
                }
            }
        }
        return mainEnemy;
    }


    private void OnMouseUpAsButton()
    {
        if (state == TowerState.BUILDING || state == TowerState.DESTROYING) return;

        UpgradeTowerManager upgradeTower = FindObjectOfType<UpgradeTowerManager>();
        upgradeTower.SelectTower(this);
    }
    public virtual IEnumerator UpgradeToLevel2()
    {
        state = TowerState.BUILDING; 
        buildingVFX.Play();
        towerMesh.mesh = levelMesh2[0];
        yield return new WaitForSeconds(buildTime);
        towerMesh.mesh = levelMesh2[1];
        state = TowerState.LEVEL_2;
        buildingVFX.Stop();
    }

    public virtual IEnumerator UpgradeToLevel3()
    {
        state = TowerState.BUILDING;
        buildingVFX.Play();
        towerMesh.mesh = levelMesh3[0];
        yield return new WaitForSeconds(buildTime);
        towerMesh.mesh = levelMesh3[1];
        state = TowerState.LEVEL_3;
        buildingVFX.Stop();
    }

    public virtual IEnumerator UpgradeToLevel4A()
    {
        state = TowerState.BUILDING;
        buildingVFX.Play();
        towerMesh.mesh = levelMesh4A[0];
        yield return new WaitForSeconds(buildTime);
        towerMesh.mesh = levelMesh4A[1];
        state = TowerState.LEVEL_4A;
        buildingVFX.Stop();
    }

    public virtual IEnumerator UpgradeToLevel4B()
    {
        state = TowerState.BUILDING;
        buildingVFX.Play();
        towerMesh.mesh = levelMesh4B[0];
        yield return new WaitForSeconds(buildTime);
        towerMesh.mesh = levelMesh4B[1];
        state = TowerState.LEVEL_4B;
        buildingVFX.Stop();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public virtual IEnumerator DestroyTower()
    {
        buildingVFX.Play();
        
        if (state == TowerState.LEVEL_1) towerMesh.mesh = levelMesh1[0];
        if (state == TowerState.LEVEL_2) towerMesh.mesh = levelMesh2[0];
        if (state == TowerState.LEVEL_3) towerMesh.mesh = levelMesh3[0];
        if (state == TowerState.LEVEL_4A) towerMesh.mesh = levelMesh4A[0];
        if (state == TowerState.LEVEL_4B) towerMesh.mesh = levelMesh4B[0];
        state = TowerState.DESTROYING;
        yield return new WaitForSeconds(5);
        builtWaypoint.buildState = BuildState.EMPTY;
        Destroy(gameObject);
    }

}
