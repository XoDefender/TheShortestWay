using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private StartEndWaypoints startEndWaypoints;
    private Pathfinder pathfinder;
    private List<WaypointData> pathToFollow;
    private TrapsController bombsController;

    private bool isGoing = false;

    private void Awake()
    {
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
        pathfinder = FindObjectOfType<Pathfinder>();
        bombsController = FindObjectOfType<TrapsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startEndWaypoints.HasPickedTargetWaypoint && !isGoing)
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
        startEndWaypoints.HasPickedTargetWaypoint = false;
        isGoing = false;

        pathfinder.DataReset();
    }

    public bool IsGoing { get { return isGoing; } }
    public List<WaypointData> PathToFollow { set { pathToFollow = value; } }
}
