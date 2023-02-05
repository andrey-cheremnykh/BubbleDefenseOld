using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{
    [SerializeField] GameObject[] archers;

    GameObject enemy;
    GameObject arrowPrefab;
    bool isReloaded = true;
    Arrow currentarrow;
    Vector3 arrowSpawnPos;
    float firerate = 1;
    float damagearrow = 10;
    ParticleSystem buildingVFX;


    // Start is called before the first frame update
    void Start()
    {
        archers[0].SetActive(false);
        archers[1].SetActive(false);
        base.Start();
    }
    void SetLevelParams(int level)
    {
        level--;
        attackRadius = TowerParams.ARROW_TOWER_RADIUS[level];
        damagearrow = TowerParams.ARROW_TOWER_DAMEGE[level];

    }
    protected override IEnumerator BuildTower()
    {
        archers[0].SetActive(false);
       yield return  StartCoroutine(base.BuildTower());
        archers[0].SetActive(true);
    }
    void Refund()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        int salePrice = 0;
        if (state == TowerState.LEVEL_1) salePrice = (int)(PricesForTowers.ARROW_1 * 0.8f);
        else if (state == TowerState.LEVEL_2) salePrice = (int)(PricesForTowers.ARROW_2 * 0.8f);
        else if (state == TowerState.LEVEL_3) salePrice = (int)(PricesForTowers.ARROW_3 * 0.8f);
        else if (state == TowerState.LEVEL_4A) salePrice = (int)(PricesForTowers.ARROW_4A * 0.8f);
        else if (state == TowerState.LEVEL_4B) salePrice = (int)(PricesForTowers.ARROW_4B * 0.8f);
        mm.AddMoney(salePrice);
    }
    public override IEnumerator DestroyTower()
    {
        Refund();
        archers[0].SetActive(false);
        archers[1].SetActive(false);
        yield return StartCoroutine(base.DestroyTower());
    }
    // Update is called once per frame
    void Update()
    {
        enemy = FindEnemyToShoot();
        if (enemy == null) return;
        if(archers[0].activeInHierarchy == true) 
        if(isReloaded)
        {

                if (state == TowerState.LEVEL_4A) StartCoroutine(ShootMassive());
            else StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {

        isReloaded = false;
        ArcherOnTower archerScript = archers[0].GetComponent<ArcherOnTower>();
        yield return StartCoroutine(archerScript.Shoot(enemy,damagearrow,firerate,arrowPrefab));
        if(archers[1].activeInHierarchy == true)
        {
        ArcherOnTower archerScript2 = archers[1].GetComponent<ArcherOnTower>();
            yield return StartCoroutine(archerScript2.Shoot(enemy, damagearrow, firerate, arrowPrefab));
        }
        isReloaded = true;
    }
    IEnumerator ShootMassive()
    {
        isReloaded = false;
        List<GameObject> enemies = FindEnemiesInRadius();
        foreach (var enemy in enemies)
        {
            Vector3 origin = transform.position + new Vector3(0, 8, 8);
            GameObject clone = Instantiate(arrowPrefab, origin, Quaternion.identity);
            clone.GetComponent<Arrow>().Launch(enemy, damagearrow);
        }
        yield return new WaitForSeconds(1 / firerate);
        isReloaded = true;
    }
    public override IEnumerator UpgradeToLevel4B()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(PricesForTowers.ARROW_4B) == false) yield break;
        archers[0].SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel4B());
        archers[0].SetActive(false);
        archers[1].SetActive(false);
        archers[0].transform.localPosition = new Vector3(2.9f, 6.7f, 1);
        archers[0].transform.localPosition = new Vector3(-2.9f, 6.7f, 1);
        SetLevelParams(5);
    }
    public override IEnumerator UpgradeToLevel4A()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(PricesForTowers.ARROW_4A) == false) yield break;
        archers[0].SetActive(false);
        yield return StartCoroutine(base.UpgradeToLevel4A());
        SetLevelParams(4);
    }
    public override IEnumerator UpgradeToLevel3()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(PricesForTowers.ARROW_3) == false) yield break;
        archers[0].SetActive(true);
        yield return StartCoroutine( base.UpgradeToLevel3());
        archers[1].SetActive(false);
        SetLevelParams(3);
    } public override IEnumerator UpgradeToLevel2()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(PricesForTowers.ARROW_2) == false) yield break; 
        archers[0].SetActive (true);
        yield return StartCoroutine( base.UpgradeToLevel2());
        archers[1].SetActive(false);
        SetLevelParams(2);
    }

    
}
