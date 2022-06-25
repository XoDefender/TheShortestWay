using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPathfinder : MonoBehaviour
{
    private EStartTargetWaypoints startTargetWaypoints;
    private WaypointData currentlyGoingFrom;
    private EnemyMovement enemyMovement;

    private Dictionary<Vector2Int, WaypointData> roadWaypoints = new Dictionary<Vector2Int, WaypointData>();
    private Dictionary<Vector2Int, WaypointData> toFrom = new Dictionary<Vector2Int, WaypointData>();

    private WaypointData[] waypoints;
    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    private Queue<WaypointData> exploringWaypoints = new Queue<WaypointData>();
    private List<WaypointData> exploredWaypoints = new List<WaypointData>();
    private List<WaypointData> path = new List<WaypointData>();
    private List<List<WaypointData>> allPaths = new List<List<WaypointData>>();

    private bool readyToFindPath = false;
    private bool isStartWaypointInQueue = false;
    private bool found = false;

    private void Awake()
    {
        startTargetWaypoints = GetComponent<EStartTargetWaypoints>();
        waypoints = FindObjectsOfType<WaypointData>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
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
        if(!enemyMovement.IsGoing)
            BreadthFirstSearch(startTargetWaypoints.StartWaypoint, startTargetWaypoints.ReadyToPickTargetWaypoint);
    }

    private void BreadthFirstSearch(WaypointData startWaypoint, bool readyToPickTargetWaypoint)
    {
        while (!found)
        {
            if (startWaypoint && !readyToPickTargetWaypoint)
            {
                if (!isStartWaypointInQueue)
                {
                    exploringWaypoints.Enqueue(startTargetWaypoints.StartWaypoint);
                    isStartWaypointInQueue = true;
                }

                if (!path.Contains(startTargetWaypoints.StartWaypoint))
                    exploredWaypoints.Add(startTargetWaypoints.StartWaypoint);

                if (!path.Contains(startTargetWaypoints.TargetWaypoint))
                    path.Add(startTargetWaypoints.TargetWaypoint);

                currentlyGoingFrom = startTargetWaypoints.TargetWaypoint;

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
                { 
                    readyToFindPath = true;
                    found = true;

                    FindPath();
                    DataReset();
                }
            }
        }
    }

    private void FindPath()
    {
        if (readyToFindPath)
        {
            if (toFrom.ContainsKey(currentlyGoingFrom.GetGridPosition()))
            {
                while (toFrom[currentlyGoingFrom.GetGridPosition()] != startTargetWaypoints.StartWaypoint)
                {
                    path.Add(toFrom[currentlyGoingFrom.GetGridPosition()]);
                    currentlyGoingFrom = toFrom[currentlyGoingFrom.GetGridPosition()];
                }
            }

            if (!path.Contains(startTargetWaypoints.StartWaypoint))
                path.Add(startTargetWaypoints.StartWaypoint);

            path.Reverse();

            List<WaypointData> tempPath = new List<WaypointData>(path);
            AllPaths.Add(tempPath);

            startTargetWaypoints.ReadyToPickTargetWaypoint = true;

            foreach (WaypointData waypoint in path)
            {
                waypoint.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
    }

    public void DataReset()
    {
        path.Clear();
        exploredWaypoints.Clear();
        toFrom.Clear();

        isStartWaypointInQueue = false;
        readyToFindPath = false;
    }

    public bool Found { set { found = value; } }
    public List<List<WaypointData>> AllPaths { get { return allPaths; } set { allPaths = value; } }
}
