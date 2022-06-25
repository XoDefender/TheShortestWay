using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EStartTargetWaypoints startTargetWaypoints;
    private EPathfinder pathfinder;
    private EPathAnalyzer pathAnalyzer;
    private PlayerMovement playerMovement;

    private List<WaypointData> pathToFollow = new List<WaypointData>();

    private bool isGoing = false;

    private void Awake()
    {
        pathfinder = FindObjectOfType<EPathfinder>();
        startTargetWaypoints = FindObjectOfType<EStartTargetWaypoints>();
        pathAnalyzer = FindObjectOfType<EPathAnalyzer>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathToFollow.Count != 0 && !isGoing && playerMovement.IsEnemyReadyToGo)
        {
            StartCoroutine(StartMovement());
            isGoing = true;
        }
    }

    IEnumerator StartMovement()
    {
        foreach (WaypointData waypoint in pathToFollow)
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

        DataReset();
    }

    private void DataReset()
    {
        startTargetWaypoints.StartWaypoint = null;
        startTargetWaypoints.ReadyToPickTargetWaypoint = true;
        startTargetWaypoints.TargetWaypointNumber = 0;

        pathfinder.AllPaths.Clear();
        pathToFollow.Clear();

        pathfinder.DataReset();

        pathAnalyzer.allPathsAreObserved = false;

        isGoing = false;
        playerMovement.IsEnemyReadyToGo = false;
    }

    public List<WaypointData> PathToFollow { set { pathToFollow = value; } }
    public bool IsGoing { get { return isGoing; } }
}
