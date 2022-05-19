using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndWaypoints : MonoBehaviour
{
    private WaypointData startWaypoint;
    private WaypointData endWaypoint;
    private WaypointData[] waypoints;
    private GameObject player;

    private const string playerName = "Player";

    private void Awake()
    {
        player = GameObject.Find(playerName);
        waypoints = FindObjectsOfType<WaypointData>();
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
        if (endWaypoint == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = endColor;
                    endWaypoint = hit.collider.gameObject.GetComponent<WaypointData>();
                }
            }
        }
    }

    public WaypointData GetStartWaypoint()
    {
        return startWaypoint;
    }

    public WaypointData GetEndWaypoint()
    {
        return endWaypoint;
    }

    public void SetStartWaypoinToNull()
    {
        startWaypoint = null;
    }

    public void SetEndWaypoinToNull()
    {
        endWaypoint = null;
    }
}
