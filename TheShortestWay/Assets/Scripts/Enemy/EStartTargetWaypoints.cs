using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStartTargetWaypoints : MonoBehaviour
{
    [SerializeField] private EWaypointData endWaypoint;

    private EWaypointData[] waypoints;
    private EWaypointData startWaypoint;
    private EWaypointData targetWaypoint;
    private GameObject enemy;
    private EPathfinder pathfinder;
    private ECoinCollector coinCollector;

    private const string enemyName = "Enemy";

    private bool readyToPickTargetWaypoint = true;
    private bool readyToPickEndWaypoint = false;
    private int targetWaypointNumber = 0;

    private void Awake()
    {
        pathfinder = FindObjectOfType<EPathfinder>();
        waypoints = FindObjectsOfType<EWaypointData>();
        enemy = GameObject.Find(enemyName);
        coinCollector = FindObjectOfType<ECoinCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        PickStartWaypoint(waypoints);
        PickTargetWaypoint();
        SetEndWaypointData();

        if (Mathf.Approximately(endWaypoint.transform.position.x, enemy.transform.position.x) 
            && Mathf.Approximately(endWaypoint.transform.position.z, enemy.transform.position.z)
            && coinCollector.PickedCoins >= coinCollector.RequiredCoins)
            Destroy(pathfinder);
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
        if (readyToPickTargetWaypoint && targetWaypointNumber < waypoints.Length && !readyToPickEndWaypoint)
        {
            targetWaypoint = waypoints[targetWaypointNumber];

            waypoints[targetWaypointNumber].SetColor(Color.red);

            targetWaypointNumber += 1;

            readyToPickTargetWaypoint = false;

            pathfinder.Found = false;
        }

        if (readyToPickEndWaypoint)
        {
            targetWaypoint = endWaypoint;
            readyToPickTargetWaypoint = false;
            pathfinder.Found = false;
        }
    }
    
    private void SetEndWaypointData()
    {
        endWaypoint.TextMesh.text = "";
        endWaypoint.GetComponent<MeshRenderer>().material.color = Color.black;
    }

    public EWaypointData StartWaypoint { get { return startWaypoint; } set { startWaypoint = value; } }
    public EWaypointData TargetWaypoint { get { return targetWaypoint; } }

    public bool ReadyToPickTargetWaypoint { get { return readyToPickTargetWaypoint; } set { readyToPickTargetWaypoint = value; } }
    public bool ReadyToPickEndWaypoint { get { return readyToPickEndWaypoint; } set { readyToPickEndWaypoint = value; } }

    public int TargetWaypointNumber { set { targetWaypointNumber = value; } }
}
