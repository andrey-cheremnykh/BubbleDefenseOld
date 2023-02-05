using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] GameObject top;
    bool isReloaded;
    float fireRate = 5;
    protected override IEnumerator BuildTower()
    { 
        top.SetActive(false);
        yield return StartCoroutine(base.BuildTower());
        top.SetActive(true);
        top.GetComponent<Top>().SetTop(1);
    }
    public override IEnumerator DestroyTower()
    {
        Refund();
        top.SetActive(false);
        yield return StartCoroutine(base.DestroyTower());
    }
    void Refund()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        int salePrice = 0;
        if (state == TowerState.LEVEL_1) salePrice = (int)(PricesForTowers.CANNON_1 * 0.8f);
        else if (state == TowerState.LEVEL_2) salePrice = (int)(PricesForTowers.CANNON_2 * 0.8f);
        else if (state == TowerState.LEVEL_3) salePrice = (int)(PricesForTowers.CANNON_3 * 0.8f);
        else if (state == TowerState.LEVEL_4A) salePrice = (int)(PricesForTowers.CANNON_4A * 0.8f);
        else if (state == TowerState.LEVEL_4B) salePrice = (int)(PricesForTowers.CANNON_4B * 0.8f);
        mm.AddMoney(salePrice);
    }
    private void Update()
    {
        if (state == TowerState.BUILDING || state == TowerState.DESTROYING) return;
        GameObject enemy = FindEnemyToShoot();
        if (!enemy) return;
        if (isReloaded)
        {
            StartCoroutine(ShootEnemy(enemy));
        }
        IEnumerator ShootEnemy(GameObject enemy)
        {
            isReloaded = true;
                top.GetComponent<Top>().ShootEnemy(enemy. transform);
            yield return new WaitForSeconds(1 / fireRate);
            isReloaded = false;
        }
    }
    public override IEnumerator UpgradeToLevel4B()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(PricesForTowers.CANNON_4B) == false) yield break;
        yield return StartCoroutine(base.UpgradeToLevel4B());
        top.SetActive(true);
        top.GetComponent<Top>().SetTop(5);
        attackRadius = TowerParams.CANNON_TOWER_RADIUS[4];
    }
    public override IEnumerator UpgradeToLevel4A()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(PricesForTowers.CANNON_4A) == false) yield break;
        yield return StartCoroutine(base.UpgradeToLevel4A());
    }
    public override IEnumerator UpgradeToLevel3()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(PricesForTowers.CANNON_3) == false) yield break;
        yield return StartCoroutine(base.UpgradeToLevel3());
    }
    public override IEnumerator UpgradeToLevel2()
    {
        MoneyManager mm = FindObjectOfType<MoneyManager>();
        if (mm.SpendMoney(PricesForTowers.CANNON_2) == false) yield break;
        yield return StartCoroutine(base.UpgradeToLevel2());
    }

}