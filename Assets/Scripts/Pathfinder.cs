using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid;
    Queue<Waypoint> searchQueue;
    List<Waypoint> path;

    bool isEndFound = false;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Dictionary<Vector2Int, Waypoint>();
        searchQueue = new Queue<Waypoint>();
        path = new List<Waypoint>();

        FillGridDictionary();
        ColorizeStartEnd();
    }

    void ColorizeStartEnd()
    {
        startWaypoint.ColorCube(Color.green);
        endWaypoint.ColorCube(Color.cyan);
    }

    void FillGridDictionary()
    {
        grid.Clear();
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector2Int waypointPos = waypoints[i].GetGridPos();
            bool isInGrid = grid.ContainsKey(waypointPos);
            if(isInGrid == true)
            {
                Debug.LogWarning("У тебя дублируется куб: "+ waypointPos);
            }
            else
            {
                waypoints[i].isExplored = false;
                if (waypoints[i].buildState != BuildState.TOWER_BUILT)
                {
                    grid.Add(waypointPos, waypoints[i]);
                    print("Добавлен куб: " + waypointPos);
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindPath();
        }
        
    }

    public List<Waypoint> FindPath()
    {
        PreparePathfinding();

        while (searchQueue.Count > 0 && isEndFound == false)
        {
            Waypoint centerSearch = searchQueue.Dequeue();
            ExploreNeightbours(centerSearch);
        }
        
        SavePath();
        return path;
    }



    void SavePath()
    {

        path.Clear();
        path.Add(endWaypoint);

        Waypoint prevPoint = endWaypoint.fromPoint;

        while (prevPoint != startWaypoint)
        {
            path.Add(prevPoint);
            prevPoint = prevPoint.fromPoint;
        }

        path.Add(startWaypoint);
        path.Reverse();
    }

    void ExploreNeightbours(Waypoint centerSearch)
    {
        Vector2Int centerPoint = centerSearch.GetGridPos();
        ExplorePoint(centerPoint + Vector2Int.up, centerSearch); 
        ExplorePoint(centerPoint + Vector2Int.left, centerSearch); 
        ExplorePoint(centerPoint + Vector2Int.down, centerSearch); 
        ExplorePoint(centerPoint + Vector2Int.right, centerSearch);
    }

    void ExplorePoint( Vector2Int point, Waypoint center )
    {
        Waypoint neighbour;
        grid.TryGetValue(point, out neighbour);

        if (neighbour == null) return;
        if (neighbour.isExplored == true) return;

        searchQueue.Enqueue(neighbour);
        neighbour.ColorCube(Color.blue);
        neighbour.isExplored = true;
        neighbour.fromPoint = center;

        if(neighbour == endWaypoint)
        {
            isEndFound = true;
            endWaypoint.ColorCube(Color.red);
        }
    }


    public bool CheckPath(Waypoint blocked)
    {
        PreparePathfinding();

        grid.Remove(blocked.GetGridPos());
        if (blocked == startWaypoint) return false;

        while (searchQueue.Count > 0 && isEndFound == false)
        {
            Waypoint centerSearch = searchQueue.Dequeue();
            ExploreNeightbours(centerSearch);
        }
        return isEndFound;
    }

    void PreparePathfinding()
    {
        isEndFound = false;
        searchQueue.Clear();
        FillGridDictionary();
        searchQueue.Enqueue(startWaypoint);
        startWaypoint.isExplored = true;
    }

}
