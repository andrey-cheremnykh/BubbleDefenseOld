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
    private void Start()
    {
        towerGui = FindObjectOfType<BuildTowerGUI>();
        selectVFX = GetComponentInChildren<ParticleSystem>().gameObject;
        selectVFX.SetActive(false);
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

    bool CheckBuildable(Waypoint waypoint)
    {
        if (waypoint.buildState != BuildState.EMPTY) return false;

        Pathfinder p = FindObjectOfType<Pathfinder>();
        bool isPathFound = p.CheckPath(waypoint);

        return isPathFound;
    }
    public void SpawnArcherTower()
    {
        if (towerGui.stateBuild == ButtonState.ACTIVE) return;
        if (lastPressedPoint.buildState != BuildState.EMPTY) return;
        Vector3 buildPoint = lastPressedPoint.transform.position + new Vector3(0,6,0);
        GameObject cloneTower = Instantiate(ArcherTowerPrefab, buildPoint, Quaternion.identity);
        lastPressedPoint.buildState = BuildState.TOWER_BUILT;
        towerGui.CreateTowerSlider(cloneTower.transform);
        DisableButtons();
    }
    public void SpawnCannonTower()
    {
           if (towerGui.stateBuild == ButtonState.ACTIVE) return;
        if (lastPressedPoint.buildState != BuildState.EMPTY) return;
        Vector3 buildPoint = lastPressedPoint.transform.position + new Vector3(0,6,0);
        GameObject cloneTower = Instantiate(CannonTowerPrefab, buildPoint, Quaternion.identity);
        lastPressedPoint.buildState = BuildState.TOWER_BUILT;
        DisableButtons();
    }public void SpawnMagicTower()
    {
        if (towerGui.stateBuild == ButtonState.ACTIVE) return;
        if (lastPressedPoint.buildState != BuildState.EMPTY) return;
        Vector3 buildPoint = lastPressedPoint.transform.position + new Vector3(0,6,0);
        GameObject cloneTower = Instantiate(MagicTowerPrefab, buildPoint, Quaternion.identity);
        lastPressedPoint.buildState = BuildState.TOWER_BUILT;
        DisableButtons();
    } 
}
