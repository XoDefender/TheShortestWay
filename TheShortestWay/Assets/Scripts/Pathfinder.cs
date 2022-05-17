using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Waypoint waypointController;
    private Dictionary<Vector2Int, Waypoint> roadWaypoints = new Dictionary<Vector2Int, Waypoint>();
    private Waypoint[] waypoints;
    private Vector2Int[] directions = { 
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    private void Awake()
    {
        waypointController = FindObjectOfType<Waypoint>();
        waypoints = FindObjectsOfType<Waypoint>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Waypoint waypoint in waypoints)
        {
            if(!roadWaypoints.ContainsKey(waypoint.GetGridPosition()))
                roadWaypoints.Add(waypoint.GetGridPosition(), waypoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        waypointController.ColorStartAndEnd(Color.white, Color.black, waypoints);

        ExploreNeighbours();
    }

    private void ExploreNeighbours()
    {
        Waypoint startWaypoint = waypointController.GetStartWaypoint(waypoints);

        foreach (Vector2Int direction in directions)
        {
            Vector2Int exploredWaypointCoordinates = startWaypoint.GetGridPosition() + direction;

            if(roadWaypoints.ContainsKey(exploredWaypointCoordinates))
                roadWaypoints[exploredWaypointCoordinates].SetColor(Color.blue);
        }
    }
}
