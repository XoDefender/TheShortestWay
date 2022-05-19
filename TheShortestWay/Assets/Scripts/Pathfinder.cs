using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private StartEndWaypoints startEndWaypoints;
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

    private bool isStartWaypointInQueue = false;

    private void Awake()
    {
        startEndWaypoints = GetComponent<StartEndWaypoints>();
        waypoints = FindObjectsOfType<WaypointData>();
    }

    void Start()
    { 
        foreach (WaypointData waypoint in waypoints)
        {
            if (!roadWaypoints.ContainsKey(waypoint.GetGridPosition()))
                roadWaypoints.Add(waypoint.GetGridPosition(), waypoint);
        }
    }

    void Update()
    {
        BreadthFirstSearch(startEndWaypoints.GetStartWaypoint(), startEndWaypoints.GetEndWaypoint());
    }

    private void BreadthFirstSearch(WaypointData startWaypoint, WaypointData endWaypoint)
    {
        if(endWaypoint && startWaypoint)
        {
            if(!isStartWaypointInQueue)
            {
                exploringWaypoints.Enqueue(startEndWaypoints.GetStartWaypoint());
                isStartWaypointInQueue = true;
            }

            if (!path.Contains(startEndWaypoints.GetStartWaypoint()))
                exploredWaypoints.Add(startEndWaypoints.GetStartWaypoint());

            if (!path.Contains(startEndWaypoints.GetEndWaypoint()))
                path.Add(startEndWaypoints.GetEndWaypoint());

            currentlyGoingFrom = startEndWaypoints.GetEndWaypoint();

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
            {
                exploringWaypoints.Dequeue();
                readyToFindPath = false;
            }
            else
                readyToFindPath = true;

            if (readyToFindPath && !readyToGetPath)
            {
                while (toFrom[currentlyGoingFrom.GetGridPosition()] != startEndWaypoints.GetStartWaypoint())
                {
                    path.Add(toFrom[currentlyGoingFrom.GetGridPosition()]);
                    currentlyGoingFrom = toFrom[currentlyGoingFrom.GetGridPosition()];
                }

                if (!path.Contains(startEndWaypoints.GetStartWaypoint()))
                    path.Add(startEndWaypoints.GetStartWaypoint());

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
