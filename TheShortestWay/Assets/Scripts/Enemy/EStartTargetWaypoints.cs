using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStartTargetWaypoints : MonoBehaviour
{
    private EWaypointData[] waypoints;
    private EWaypointData startWaypoint;
    private EWaypointData targetWaypoint;
    private GameObject enemy;

    private const string enemyName = "Enemy";

    private bool readyToPickTargetWaypoint = true;
    private int targetWaypointNumber = 0;

    private void Awake()
    {
        waypoints = FindObjectsOfType<EWaypointData>();
        enemy = GameObject.Find(enemyName);
    }

    // Update is called once per frame
    void Update()
    {
        PickStartWaypoint(waypoints);
        PickTargetWaypoint();
    }

    private void PickStartWaypoint(EWaypointData[] waypoints)
    {
        if (startWaypoint == null)
        {
            foreach (EWaypointData waypoint in waypoints)
            {
                if (Mathf.Approximately(waypoint.transform.position.x, enemy.transform.position.x) && Mathf.Approximately(waypoint.transform.position.z, enemy.transform.position.z))
                {
                    startWaypoint = waypoint;

                    break;
                }
            }
        }
    }

    private void PickTargetWaypoint()
    {
        if (readyToPickTargetWaypoint && targetWaypointNumber < waypoints.Length)
        {
            targetWaypoint = waypoints[targetWaypointNumber];

            waypoints[targetWaypointNumber].SetColor(Color.red);

            targetWaypointNumber += 1;

            readyToPickTargetWaypoint = false;
        }
    }

    public EWaypointData StartWaypoint { get { return startWaypoint; } set { startWaypoint = value; } }
    public EWaypointData TargetWaypoint { get { return targetWaypoint; } }

    public bool ReadyToPickTargetWaypoint { get { return readyToPickTargetWaypoint; } set { readyToPickTargetWaypoint = value; } }
}
