using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BuildState
{
    NON_BUILD,
    EMPTY,
    TOWER_BUILT
}

[SelectionBase]
public class Waypoint : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] int gridScale = 10;

    public bool isExplored = false;

    public Waypoint fromPoint;

    public BuildState buildState = BuildState.EMPTY;


    public int GetGridScale()
    {
        return gridScale;
    }

    public Vector2Int GetGridPos()
    {
        Vector2Int pos = new Vector2Int();
        pos.x = Mathf.RoundToInt(transform.position.x / gridScale);
        pos.y = Mathf.RoundToInt(transform.position.z / gridScale);
        return pos;
    }

    public void ColorCube(Color c)
    {
        GetComponent<MeshRenderer>().material.color = c;
    }


    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        BuildTowerManager towerManager = FindObjectOfType<BuildTowerManager>();
        towerManager.BuildTower(this);
    }

}
