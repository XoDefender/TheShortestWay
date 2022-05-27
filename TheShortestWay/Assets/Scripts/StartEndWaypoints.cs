using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndWaypoints : MonoBehaviour
{
    [SerializeField] private LayerMask waypointLayerMask;

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
        if (!hasPickedEndWaypoint && readyToPickEndWaypoint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, waypointLayerMask))
            {
                endWaypoint = hit.collider.gameObject.GetComponent<WaypointData>();

                readyToPickEndWaypoint = false;
                pathfinder.IsObserved = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100, waypointLayerMask))
                    hasPickedEndWaypoint = true;
            }
        }
    }

    public WaypointData StartWaypoint { get { return startWaypoint; } set { startWaypoint = null; } }

    public WaypointData EndWaypoint { get { return endWaypoint; } set { endWaypoint = null; } }

    public bool HasPickedEndWaypoint { get { return hasPickedEndWaypoint; } set { hasPickedEndWaypoint = value; } }
}
