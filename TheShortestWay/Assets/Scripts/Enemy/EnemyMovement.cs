using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private List<EWaypointData> pathToFollow;
    private EPathfinder pathfinder;
    private EStartTargetWaypoints startTargetWaypoints;

    private bool isGoing = false;

    private void Awake()
    {
        pathfinder = FindObjectOfType<EPathfinder>();
        startTargetWaypoints = FindObjectOfType<EStartTargetWaypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathToFollow != null && !isGoing && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartMovement());
            isGoing = true;
        }
    }

    IEnumerator StartMovement()
    {
        foreach (EWaypointData waypoint in pathToFollow)
        {
            Vector3 targetVector = new Vector3(waypoint.transform.position.x, transform.position.y, waypoint.transform.position.z) - transform.position;
            Vector3 startPosition = transform.position;

            if (targetVector != Vector3.zero)
                transform.forward = targetVector;

            int movingSpeed = 20;

            for (int i = 1; i <= movingSpeed; i++)
            {
                float t = 1f * i / movingSpeed;

                transform.position = startPosition + targetVector * t;

                yield return null;
            }

            yield return null;
        }

        startTargetWaypoints.StartWaypoint = null;
        startTargetWaypoints.ReadyToPickTargetWaypoint = true;
        startTargetWaypoints.TargetWaypointNumber = 0;

        pathfinder.AllPaths.Clear();
        pathfinder.DataReset();
        pathToFollow = null;

        isGoing = false;
    }

    public List<EWaypointData> PathToFollow { set { pathToFollow = value; } }
    public bool IsGoing { get { return isGoing; } }
}
