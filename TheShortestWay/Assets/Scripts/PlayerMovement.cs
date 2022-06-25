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
    private bool isEnemyReadyToGo = false;

    private void Awake()
    {
        startEndWaypoints = FindObjectOfType<StartEndWaypoints>();
        pathfinder = FindObjectOfType<Pathfinder>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startEndWaypoints.HasPickedTargetWaypoint && !isGoing && !isEnemyReadyToGo)
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
            
            if(targetVector != Vector3.zero)
                transform.forward = targetVector;

            int movingSpeed = 170;

            for(int i = 1; i <= movingSpeed; i++)
            {
                float t = 1f * i / movingSpeed;

                transform.position = startPosition + targetVector * t;

                if(i == 20)
                {
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsIdling", false);
                }
                
                if(i == movingSpeed - 5)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsIdling", true);
                }

                yield return null;
            }

            yield return null; 
        }

        startEndWaypoints.StartWaypoint = null;
        startEndWaypoints.HasPickedTargetWaypoint = false;
        isGoing = false;
        isEnemyReadyToGo = true;

        pathfinder.DataReset();
    }

    public List<WaypointData> PathToFollow { set { pathToFollow = value; } }
    public bool IsEnemyReadyToGo { get { return isEnemyReadyToGo; } set { isEnemyReadyToGo = value; } }
}
