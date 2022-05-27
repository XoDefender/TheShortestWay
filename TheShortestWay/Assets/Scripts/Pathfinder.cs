using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private StartEndWaypoints startEndWaypoints;
    private WaypointData currentlyGoingFrom;
    private WaypointData[] waypoints;

    public Dictionary<Vector2Int, WaypointData> roadWaypoints = new Dictionary<Vector2Int, WaypointData>();
    public Dictionary<Vector2Int, WaypointData> toFrom = new Dictionary<Vector2Int, WaypointData>();

    public Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public Queue<WaypointData> exploringWaypoints = new Queue<WaypointData>();
    public List<WaypointData> exploredWaypoints = new List<WaypointData>();
    public List<WaypointData> path = new List<WaypointData>();

    private PlayerMovement playerMovement;

    public bool readyToFindPath = false;

    public bool isStartWaypointInQueue = false;

    public bool isObserved = false;
    public bool areEqual = true;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
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
        BreadthFirstSearch(startEndWaypoints.StartWaypoint, startEndWaypoints.EndWaypoint);
    }

    private void BreadthFirstSearch(WaypointData startWaypoint, WaypointData endWaypoint)
    {
        if (endWaypoint && startWaypoint && !isObserved)
        {
            if (!isStartWaypointInQueue)
            {
                exploringWaypoints.Enqueue(startEndWaypoints.StartWaypoint);
                isStartWaypointInQueue = true;
            }

            if (!path.Contains(startEndWaypoints.StartWaypoint))
                exploredWaypoints.Add(startEndWaypoints.StartWaypoint);

            if (!path.Contains(startEndWaypoints.EndWaypoint))
                path.Add(startEndWaypoints.EndWaypoint);

            currentlyGoingFrom = startEndWaypoints.EndWaypoint;

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
                           /*roadWaypoints[exploredWaypointCoordinates].SetColor(Color.blue);*/

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

            if (readyToFindPath)
            {
                if(toFrom.ContainsKey(currentlyGoingFrom.GetGridPosition()))
                {
                    while (toFrom[currentlyGoingFrom.GetGridPosition()] != startEndWaypoints.StartWaypoint)
                    {
                        path.Add(toFrom[currentlyGoingFrom.GetGridPosition()]);
                        currentlyGoingFrom = toFrom[currentlyGoingFrom.GetGridPosition()];
                    }
                }
                

                if (!path.Contains(startEndWaypoints.StartWaypoint))
                    path.Add(startEndWaypoints.StartWaypoint);

                path.Reverse();

                foreach (WaypointData waypoint in path)
                {
                    waypoint.GetComponent<MeshRenderer>().material.color = Color.red;
                }

                List<WaypointData> tempPath = new List<WaypointData>(path);
                playerMovement.pathToFollow = tempPath;

                isObserved = true;
                startEndWaypoints.readyToPickEndWaypoint = true;
            }
        }
        else
        {
            DataReset();
        }

        if(areEqual && isObserved)
        {
            path.Clear();
        }

        if(isObserved)
        {
            if(!areEqual)
            {
                DataReset();
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

        foreach(WaypointData waypoint in waypoints)
        {
            waypoint.GetComponent<MeshRenderer>().material.color = Color.grey;
        }
    }
}
