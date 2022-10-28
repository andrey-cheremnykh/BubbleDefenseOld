using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerManager : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    BuildTowerGui towerGui;
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
        GameObject newTower = Instantiate(towerPrefab, buildPoint, Quaternion.identity);

        waypoint.buildState = BuildState.TOWER_BUILT;
    }

    bool CheckBuildable(Waypoint waypoint)
    {
        if (waypoint.buildState != BuildState.EMPTY) return false;

        Pathfinder p = FindObjectOfType<Pathfinder>();
        bool isPathFound = p.CheckPath(waypoint);

        return isPathFound;
    }

}
