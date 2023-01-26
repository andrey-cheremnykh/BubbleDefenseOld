using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerManager : MonoBehaviour
{
    [SerializeField] GameObject ArcherTowerPrefab;
    [SerializeField] GameObject CannonTowerPrefab;
    [SerializeField] GameObject MagicTowerPrefab;

    BuildTowerGUI towerGui;
    Waypoint lastPressedPoint;
    GameObject selectVFX;
    int currentTowerCount;
    int maxToerCount = 5;
    EnemySpawner enemySpawner;
    MoneyManager mm;
    private void Start()
    {
        towerGui = FindObjectOfType<BuildTowerGUI>();
        selectVFX = GetComponentInChildren<ParticleSystem>().gameObject;
        mm = FindObjectOfType<MoneyManager>();
        SetMAxTowerCount();
        selectVFX.SetActive(false);
    }
    void SetMAxTowerCount()
    {
        int countLevel = PlayerPrefs.GetInt("tower-limit");
        if (countLevel == 1) maxToerCount = 8;
        if (countLevel == 2) maxToerCount = 12;
        if (countLevel == 3) maxToerCount = 16;
        if (countLevel == 4) maxToerCount = 20;
        print($"Max Towers: {maxToerCount}");
    }
    public void DecreaseTowerCount()
    {
        currentTowerCount--;
    }

    public void BuildTower(Waypoint waypoint)
    {
        bool isBuildable = CheckBuildable(waypoint);
        if (isBuildable == false) return;
        Vector3 buildPoint = waypoint.transform.position + new Vector3(0, 6, 0);
        StartCoroutine(towerGui.SetBuildButtons(buildPoint));
        GameObject newTower = Instantiate(ArcherTowerPrefab, buildPoint, Quaternion.identity);

        waypoint.buildState = BuildState.TOWER_BUILT;
    }
    public void DisableButtons()
    {
        if (towerGui.stateBuild != ButtonState.ACTIVE) return;
        StartCoroutine(towerGui.DisableBuildButtons());
        selectVFX.SetActive(false);
    }
    public void SetBuildPoint(Waypoint way) 
    {
        Vector3 offset = new(0,0.05f,0);
        bool isBuildable = CheckBuildable(way);
            if (isBuildable == false) return;
        if (towerGui.stateBuild != ButtonState.DISABLED) return;
        lastPressedPoint = way;
        Vector3 buildPoint = way.transform.position;
        selectVFX.SetActive(true);
        selectVFX.transform.position = buildPoint + offset;
        StartCoroutine(towerGui.SetBuildButtons(buildPoint)); 


    }
    public void SpawnArcherTower()
    {

        bool canBuild = CheckSpawnTower(ArcherTowerPrefab.GetComponent<Tower>());
        if (canBuild)
        {
            BuildTower(ArcherTowerPrefab);
        }
    }
    public void SpawnCannonTower()
    {
        bool canBuild = CheckSpawnTower(CannonTowerPrefab.GetComponent<Tower>());
        if (canBuild)
        {
            BuildTower(CannonTowerPrefab);
        }

    }
    public void SpawnMagicTower()
    {
        bool canBuild = CheckSpawnTower(MagicTowerPrefab.GetComponent<Tower>());
        if (canBuild)
        {
            BuildTower(MagicTowerPrefab);
        }
    } 

    bool CheckBuildable(Waypoint waypoint)
    {
        if (waypoint.buildState != BuildState.EMPTY) return false;

        Pathfinder p = FindObjectOfType<Pathfinder>();
        bool isPathFound = p.CheckPath(waypoint);

        return isPathFound;
    }
    bool CheckSpawnTower(Tower buildingTower)
    {
        if (currentTowerCount >= maxToerCount) return false;
        if (towerGui.stateBuild == ButtonState.ACTIVE) return false;
        if (lastPressedPoint.buildState != BuildState.EMPTY) return false;
        if (enemySpawner.state != SpawnState.NON_SPAWNING) return false;
        if(buildingTower is ArrowTower) 
        {
            if (mm.SpendMoney(PricesForTowers.ARROW_1) == false) return false;
        }
        else if(buildingTower is CannonTower) 
        {
            if (mm.SpendMoney(PricesForTowers.CANNON_1) == false) return false;
        }
        else if(buildingTower is MagicTower) 
        {
            if (mm.SpendMoney(PricesForTowers.MAGIC_1) == false) return false;
        }
        return true;  
    }
    void BuildTower(GameObject towerPrefab)
    {
        currentTowerCount++;
        Vector3 buildPoint = lastPressedPoint.transform.position + new Vector3(0,6,0);
        GameObject cloneTower = Instantiate(MagicTowerPrefab, buildPoint, Quaternion.identity);
        lastPressedPoint.buildState = BuildState.TOWER_BUILT;
        DisableButtons();

    }
}
