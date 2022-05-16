using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Dictionary<Vector2Int, Waypoint> roadWaypoints = new Dictionary<Vector2Int, Waypoint>();
    private Waypoint[] waypoints;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            if(!roadWaypoints.ContainsKey(waypoint.GetGridPosition()))
                roadWaypoints.Add(waypoint.GetGridPosition(), waypoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<Waypoint>().ColorStartAndEnd(Color.white, Color.black, waypoints);
    }
}
