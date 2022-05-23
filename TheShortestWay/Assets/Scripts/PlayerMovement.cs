using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Pathfinder pathfinder;
    private StartEndWaypoints startEndWaypoints;

    private bool isGoing = false;

    private void Awake()
    {
        pathfinder = GameObject.Find("Road").GetComponent<Pathfinder>();
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGoing && pathfinder.readyToGetPath && startEndWaypoints.HasPickedEndWaypoint)
        {
            Debug.Log(isGoing + "  " + pathfinder.readyToGetPath + "  " + startEndWaypoints.HasPickedEndWaypoint);
            StartCoroutine(StartMovement());
            isGoing = true;
        }
    }

    IEnumerator StartMovement()
    {
        foreach (WaypointData waypoint in pathfinder.Path)
        {
            transform.position = new Vector3(waypoint.transform.position.x, transform.position.y, waypoint.transform.position.z);

            yield return new WaitForSeconds(1);
        }

        startEndWaypoints.HasPickedEndWaypoint = false;
        startEndWaypoints.StartWaypoint = null;
    }

    public bool IsGoing { set { isGoing = value; } }
}
