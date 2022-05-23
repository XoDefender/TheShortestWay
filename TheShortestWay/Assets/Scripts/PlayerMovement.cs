using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private StartEndWaypoints startEndWaypoints;
    private List<WaypointData> pathToFollow;

    private bool isGoing = false;

    private void Awake()
    {
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pathToFollow.Count);
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
    }

    public List<WaypointData> PathToFollow { get { return pathToFollow; } set { pathToFollow = value; } }
}
