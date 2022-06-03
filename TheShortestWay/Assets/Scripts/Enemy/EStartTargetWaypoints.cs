using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStartTargetWaypoints : MonoBehaviour
{
    private EWaypointData[] waypoints;

    private bool readyToPickTargetWaypoint = true;
    private int targetWaypointNumber = 0;

    private void Awake()
    {
        waypoints = FindObjectsOfType<EWaypointData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(readyToPickTargetWaypoint && targetWaypointNumber < waypoints.Length)
        {
            waypoints[targetWaypointNumber].SetColor(Color.red);

            targetWaypointNumber += 1;
        }
    }
}
