using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerManager : MonoBehaviour
{
    [SerializeField] GameObject ArcherTowerPrefab;
    [SerializeField] GameObject CannonTowerPrefab;
    [SerializeField] GameObject MagicTowerPrefab;
    BuildTowerGui towerGui;
    Waypoint lastPressedPoint;
    private void Start()
    {
        towerGui = FindObjectOfType<BuildTowerGui>();
    }

    public void BuildTower(Waypoint waypoint)
    {
        bool isBuildable = CheckBuildable(waypoint);
        if (isBuildable == false) return;

        Vector3 buildPoint = waypoint.transform.position + new Vector3(0, 6, 0);
        StartCoroutine(towerGui.SetBuildbuttons(buildPoint));
        GameObject newTower = Instantiate(ArcherTowerPrefab, buildPoint, Quaternion.identity);

        waypoint.buildState = BuildState.TOWER_BUILT;
    }
    public void DisableAllButtons()
    {
        StartCoroutine(towerGui.DisableBuildButtons());
    }
    public void SetBuildPoint(Waypoint way)
    {
        bool isBuildable = CheckBuildable(way);
            if (isBuildable == false) return;
        lastPressedPoint = way;
        Vector3 buildPoint = way.transform.position;
        StartCoroutine(towerGui.SetBuildbuttons(buildPoint)); 


    }

    bool CheckBuildable(Waypoint waypoint)
    {
        if (waypoint.buildState != BuildState.EMPTY) return false;

        Pathfinder p = FindObjectOfType<Pathfinder>();
        bool isPathFound = p.CheckPath(waypoint);

        return isPathFound;
    }
    public void SpawnCannonTower()
    {
        if (towerGui.stateBuild == BuildButtonState.ACTIVE) return;
        Vector3 buildPoint = lastPressedPoint.transform.position + new Vector3(0,6,0);
        GameObject cloneTower = Instantiate(ArcherTowerPrefab, buildPoint, Quaternion.identity);
    }
}
