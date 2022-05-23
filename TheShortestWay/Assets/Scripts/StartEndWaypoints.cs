using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndWaypoints : MonoBehaviour
{
    private WaypointData startWaypoint;
    private WaypointData endWaypoint;
    private WaypointData previousEndWaypoint;
    private WaypointData[] waypoints;
    private GameObject player;
    private Pathfinder pathfinder;

    private const string playerName = "Player";

    private bool hasPickedEndWaypoint = false;

    private void Awake()
    {
        player = GameObject.Find(playerName);
        pathfinder = GameObject.Find("Road").GetComponent<Pathfinder>();
        waypoints = FindObjectsOfType<WaypointData>();
    }

    void Update()
    {
        if (!AreEqual())
            pathfinder.Path = null;

        PickStartWaypoint(waypoints);
        PickEndWaypoint();
    }

    private void PickStartWaypoint(WaypointData[] waypoints)
    {
        if (startWaypoint == null)
        {
            foreach (WaypointData waypoint in waypoints)
            {
                if (Mathf.Approximately(waypoint.transform.position.x, player.transform.position.x) && Mathf.Approximately(waypoint.transform.position.z, player.transform.position.z))
                {
                    startWaypoint = waypoint;
                    break;
                }
            }
        }
    }

    private void PickEndWaypoint()
    {
        if(!hasPickedEndWaypoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                endWaypoint = hit.collider.gameObject.GetComponent<WaypointData>();

                if(!AreEqual())
                    pathfinder.hasPath = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100))
                    hasPickedEndWaypoint = true;
            }
        }
    }

    public bool AreEqual()
    {
        if (endWaypoint == previousEndWaypoint)
            return true;
        else
        {
            previousEndWaypoint = endWaypoint;
            return false;
        } 
    }

    public bool HasPickedEndWaypoint { get { return hasPickedEndWaypoint; } set { hasPickedEndWaypoint = value; } }

    public WaypointData StartWaypoint { get { return startWaypoint; } set { startWaypoint = null; } }

    public WaypointData EndWaypoint { get { return endWaypoint; } set { endWaypoint = null; } }
}
