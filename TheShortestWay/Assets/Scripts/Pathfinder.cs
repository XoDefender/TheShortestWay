using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private WaypointData waypointData;
    private WaypointData currentlyGoingFrom;
    private WaypointData[] waypoints;

    private Dictionary<Vector2Int, WaypointData> roadWaypoints = new Dictionary<Vector2Int, WaypointData>();
    private Dictionary<Vector2Int, WaypointData> toFrom = new Dictionary<Vector2Int, WaypointData>();

    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    private Queue<WaypointData> exploringWaypoints = new Queue<WaypointData>();
    private List<WaypointData> exploredWaypoints = new List<WaypointData>();
    private List<WaypointData> path = new List<WaypointData>();

    private bool readyToFindPath = false;
    public bool readyToGetPath = false;

    private void Awake()
    {
        waypointData = FindObjectOfType<WaypointData>();
        waypoints = FindObjectsOfType<WaypointData>();
    }

    void Start()
    {
        waypointData.PickStartWaypoint(Color.white, waypoints);

        exploringWaypoints.Enqueue(waypointData.GetStartWaypoint());
        exploredWaypoints.Add(waypointData.GetStartWaypoint());
        
        foreach (WaypointData waypoint in waypoints)
        {
            if (!roadWaypoints.ContainsKey(waypoint.GetGridPosition()))
                roadWaypoints.Add(waypoint.GetGridPosition(), waypoint);
        }
    }

    void Update()
    {
        waypointData.PickEndWaypoint(Color.black);

        BreadthFirstSearch(waypointData.GetStartWaypoint(), waypointData.GetEndWaypoint());
    }

    private void BreadthFirstSearch(WaypointData startWaypoint, WaypointData endWaypoint)
    {
        if(endWaypoint && startWaypoint)
        {
            if (!path.Contains(waypointData.GetEndWaypoint()))
                path.Add(waypointData.GetEndWaypoint());

            currentlyGoingFrom = waypointData.GetEndWaypoint();

            foreach (Vector2Int direction in directions)
            {
                if (exploringWaypoints.Count != 0)
                {
                    Vector2Int exploredWaypointCoordinates = exploringWaypoints.Peek().GetGridPosition() + direction;

                    if (roadWaypoints.ContainsKey(exploredWaypointCoordinates))
                    {
                        if (!exploredWaypoints.Contains(roadWaypoints[exploredWaypointCoordinates]) && !exploringWaypoints.Contains(roadWaypoints[exploredWaypointCoordinates]))
                        {
                            exploringWaypoints.Enqueue(roadWaypoints[exploredWaypointCoordinates]);
                            exploredWaypoints.Add(roadWaypoints[exploredWaypointCoordinates]);
                            roadWaypoints[exploredWaypointCoordinates].SetColor(Color.blue);

                            toFrom.Add(exploredWaypointCoordinates, exploringWaypoints.Peek());
                        }
                    }
                }
            }

            if (exploringWaypoints.Count != 0)
                exploringWaypoints.Dequeue();
            else
                readyToFindPath = true;

            if (readyToFindPath && !readyToGetPath)
            {
                while (toFrom[currentlyGoingFrom.GetGridPosition()] != waypointData.GetStartWaypoint())
                {
                    path.Add(toFrom[currentlyGoingFrom.GetGridPosition()]);
                    currentlyGoingFrom = toFrom[currentlyGoingFrom.GetGridPosition()];
                }

                if (!path.Contains(waypointData.GetStartWaypoint()))
                    path.Add(waypointData.GetStartWaypoint());

                path.Reverse();

                readyToGetPath = true;
            }
        }
    }

    public List<WaypointData> GetPath()
    {
        return path;
    }
}
