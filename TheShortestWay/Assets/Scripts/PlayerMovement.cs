using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private StartEndWaypoints startEndWaypoints;
    private Pathfinder pathfinder;
    public List<WaypointData> pathToFollow;

    private bool isGoing = false;

    private void Awake()
    {
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startEndWaypoints.HasPickedEndWaypoint && !isGoing)
        {
            StartCoroutine(StartMovement());
            isGoing = true;
        }
    }

    IEnumerator StartMovement()
    {
        foreach (WaypointData waypoint in pathToFollow)
        {
            transform.position = new Vector3(waypoint.transform.position.x, transform.position.y, waypoint.transform.position.z);

            yield return new WaitForSeconds(1);
        }

        startEndWaypoints.StartWaypoint = null;
        startEndWaypoints.HasPickedEndWaypoint = false;
        isGoing = false;

        pathfinder.path.Clear();
        pathfinder.exploredWaypoints.Clear();
        pathfinder.toFrom.Clear();

        pathfinder.isStartWaypointInQueue = false;
        pathfinder.readyToFindPath = false;
    }
}
