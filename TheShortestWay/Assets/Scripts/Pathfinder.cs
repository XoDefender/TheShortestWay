using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Waypoint waypointController;
    private Waypoint endWaypoint;
    private Waypoint startWaypoint;
    private Waypoint currentlyGoingFrom;
    private Waypoint[] waypoints;
    private Dictionary<Vector2Int, Waypoint> roadWaypoints = new Dictionary<Vector2Int, Waypoint>();
    private Dictionary<Vector2Int, Waypoint> toFrom = new Dictionary<Vector2Int, Waypoint>();
    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
    private Queue<Waypoint> exploringWaypoints = new Queue<Waypoint>();
    private List<Waypoint> exploredWaypoints = new List<Waypoint>();
    private List<Waypoint> path = new List<Waypoint>();

    private bool readyToFindPath = false;
    public bool readyToGetPath = false;

    private void Awake()
    {
        waypointController = FindObjectOfType<Waypoint>();
        waypoints = FindObjectsOfType<Waypoint>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startWaypoint = waypointController.GetStartWaypoint(waypoints);

        exploringWaypoints.Enqueue(startWaypoint);
        exploredWaypoints.Add(waypointController.GetStartWaypoint(waypoints));
        
        foreach (Waypoint waypoint in waypoints)
        {
            if (!roadWaypoints.ContainsKey(waypoint.GetGridPosition()))
                roadWaypoints.Add(waypoint.GetGridPosition(), waypoint);
        }

        path.Add(roadWaypoints[new Vector2Int(6, 4)]);
    }

    // Update is called once per frame
    void Update()
    {
        endWaypoint = roadWaypoints[new Vector2Int(6, 4)];
        currentlyGoingFrom = endWaypoint;

        waypointController.ColorStartAndEnd(Color.white, Color.black, waypoints);

        if (waypointController.GetHasPickedEndColorValue())
            ExploreNeighbours();

        if (readyToFindPath && !readyToGetPath)
            FindPath();
    }

    private void ExploreNeighbours()
    {
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
    }

    private void FindPath()
    {
        while(toFrom[currentlyGoingFrom.GetGridPosition()] != startWaypoint)
        {
            path.Add(toFrom[currentlyGoingFrom.GetGridPosition()]);
            currentlyGoingFrom = toFrom[currentlyGoingFrom.GetGridPosition()];
        }

        if(!path.Contains(startWaypoint))
            path.Add(startWaypoint);

        path.Reverse();

        readyToGetPath = true;
    }

    public List<Waypoint> GetPath()
    {
        return path;
    }
}
