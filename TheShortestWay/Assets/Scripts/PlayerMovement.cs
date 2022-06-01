using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private StartEndWaypoints startEndWaypoints;
    private Pathfinder pathfinder;
    private List<WaypointData> pathToFollow;
    private Animator animator;

    private bool isGoing = false;

    private void Awake()
    {
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
        pathfinder = FindObjectOfType<Pathfinder>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startEndWaypoints.HasPickedTargetWaypoint && !isGoing)
        {
            StartCoroutine(StartMovement());
            isGoing = true;

            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdling", false);
        }
    }

    IEnumerator StartMovement()
    {
        foreach (WaypointData waypoint in pathToFollow)
        {
            Vector3 targetVector = new Vector3(waypoint.transform.position.x, transform.position.y, waypoint.transform.position.z) - transform.position;
            Vector3 startPosition = transform.position;

            int iterationsCount = 60;

            for(int i = 1; i <= iterationsCount; i++)
            {
                float t = 1f * i / iterationsCount;

                transform.position = startPosition + targetVector * t;

                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        startEndWaypoints.StartWaypoint = null;
        startEndWaypoints.HasPickedTargetWaypoint = false;
        isGoing = false;

        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdling", true);

        pathfinder.DataReset();
    }

    public bool IsGoing { get { return isGoing; } }
    public List<WaypointData> PathToFollow { set { pathToFollow = value; } }
}
