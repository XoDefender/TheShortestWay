using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStartTargetWaypoints : MonoBehaviour
{
    [SerializeField] private WaypointData endWaypoint;

    private WaypointData[] waypoints;
    private WaypointData startWaypoint;
    private WaypointData targetWaypoint;
    private GameObject enemy;
    private GameObject player;
    private EPathfinder pathfinder;
    private ECoinCollector coinCollector;
    private ChestController chestController;

    private const string enemyName = "Enemy";
    private const string playerName = "Player";

    private bool readyToPickTargetWaypoint = true;
    private int targetWaypointNumber = 0;

    private void Awake()
    {
        enemy = GameObject.Find(enemyName);
        player = GameObject.Find(playerName);
        pathfinder = FindObjectOfType<EPathfinder>();
        waypoints = FindObjectsOfType<WaypointData>();
        coinCollector = FindObjectOfType<ECoinCollector>();
        chestController = FindObjectOfType<ChestController>();
    }

    // Update is called once per frame
    void Update()
    {
        PickStartWaypoint(waypoints);
        PickTargetWaypoint();
        SetEndWaypointData();

        if (Mathf.Approximately(endWaypoint.transform.position.x, enemy.transform.position.x)
            && Mathf.Approximately(endWaypoint.transform.position.z, enemy.transform.position.z)
            && coinCollector.PickedCoins >= chestController.RequiredAmountOfPoints && chestController.HasOpened)
            Destroy(player);
    }

    private void PickStartWaypoint(WaypointData[] waypoints)
    {
        if (startWaypoint == null)
        {
            foreach (WaypointData waypoint in waypoints)
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
        if (readyToPickTargetWaypoint && targetWaypointNumber < waypoints.Length)
        {
            targetWaypoint = waypoints[targetWaypointNumber];

            waypoints[targetWaypointNumber].SetColor(Color.red);

            targetWaypointNumber += 1;

            readyToPickTargetWaypoint = false;

            pathfinder.Found = false;
        }
    }
    
    private void SetEndWaypointData()
    {
        endWaypoint.TextMesh.text = "";
        endWaypoint.GetComponent<MeshRenderer>().material.color = Color.black;
    }

    public WaypointData StartWaypoint { get { return startWaypoint; } set { startWaypoint = value; } }
    public WaypointData EndWaypoint { get { return endWaypoint; } }
    public WaypointData TargetWaypoint { get { return targetWaypoint; } }

    public bool ReadyToPickTargetWaypoint { get { return readyToPickTargetWaypoint; } set { readyToPickTargetWaypoint = value; } }

    public int TargetWaypointNumber { set { targetWaypointNumber = value; } }
}
