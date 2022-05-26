using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndWaypoints : MonoBehaviour
{
    private WaypointData startWaypoint;
    private WaypointData endWaypoint;
    private WaypointData[] waypoints;
    private GameObject player;
    private Pathfinder pathfinder;

    private const string playerName = "Player";

    private bool hasPickedEndWaypoint = false;
    public bool readyToPickEndWaypoint = true;

    private void Awake()
    {
        player = GameObject.Find(playerName);
        waypoints = FindObjectsOfType<WaypointData>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Update()
    {
        PickStartWaypoint(Color.white, waypoints);
        PickEndWaypoint(Color.black);
    }

    private void PickStartWaypoint(Color startColor, WaypointData[] waypoints)
    {
        if (startWaypoint == null)
        {
            foreach (WaypointData waypoint in waypoints)
            {
                if (Mathf.Approximately(waypoint.transform.position.x, player.transform.position.x) && Mathf.Approximately(waypoint.transform.position.z, player.transform.position.z))
                {
                    waypoint.GetComponent<MeshRenderer>().material.color = startColor;
                    startWaypoint = waypoint;

                    break;
                }
            }
        }
    }

    private void PickEndWaypoint(Color endColor)
    {
        if (!hasPickedEndWaypoint && readyToPickEndWaypoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                endWaypoint = hit.collider.gameObject.GetComponent<WaypointData>();
                endWaypoint.GetComponent<MeshRenderer>().material.color = endColor;

                readyToPickEndWaypoint = false;
                pathfinder.isObserved = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100))
                    hasPickedEndWaypoint = true;
            }
        }
    }

    public WaypointData StartWaypoint { get { return startWaypoint; } set { startWaypoint = null; } }

    public WaypointData EndWaypoint { get { return endWaypoint; } set { endWaypoint = null; } }

    public bool HasPickedEndWaypoint { get { return hasPickedEndWaypoint; } set { hasPickedEndWaypoint = value; } }
}
