using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECoinCollector : MonoBehaviour
{
    private WaypointData[] waypoints;
    private EPathfinder pathfinder;
    private EnemyMovement enemyMovement;
    private EStartTargetWaypoints startTargetWaypoints;
    private EPathAnalyzer pathAnalyzer;
    private ChestController chestController;
    private WaypointData nearChestWaypoint = null;

    private int pathCoins = 0;
    private int maxCoins = 0;
    private int pickedCoins = 0;
    private int requiredCoins = 200;

    private void Awake()
    {
        waypoints = FindObjectsOfType<WaypointData>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
        pathfinder = FindObjectOfType<EPathfinder>();
        startTargetWaypoints = FindObjectOfType<EStartTargetWaypoints>();
        pathAnalyzer = FindObjectOfType<EPathAnalyzer>();
        chestController = FindObjectOfType<ChestController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pickedCoins);

        if (pickedCoins <= requiredCoins)
            SelectTheGreatestPath();
        else if(pathfinder.AllPaths.Count == waypoints.Length)
        {
            if(chestController.HasOpened)
                pathAnalyzer.FindPathToTarget(pathfinder.AllPaths, startTargetWaypoints.EndWaypoint, enemyMovement);
            else
            {
                if (nearChestWaypoint == null)
                    FindNearChestWaypoint();
                else
                    pathAnalyzer.FindPathToTarget(pathfinder.AllPaths, nearChestWaypoint, enemyMovement);
            }
        }
            
        PickUpCoin(waypoints);
    }

    private void FindNearChestWaypoint()
    {
        foreach (WaypointData waypoint in waypoints)
        {
            Vector2 chestPosition = new Vector2(chestController.transform.position.x, chestController.transform.position.z);
            Vector2 exploringWaypointPosition = new Vector2(waypoint.transform.position.x, waypoint.transform.position.z);

            if (chestPosition - exploringWaypointPosition == new Vector2(0, 10))
            {
                nearChestWaypoint = waypoint;
                break;
            }    
        }
    }

    private void SelectTheGreatestPath()
    {
        if (pathfinder.AllPaths.Count == waypoints.Length && !enemyMovement.IsGoing)
        {
            foreach (List<WaypointData> path in pathfinder.AllPaths)
            {
                if (!pathAnalyzer.HasTraps(path))
                {
                    CountCoins(path);

                    if (pathCoins > maxCoins)
                        maxCoins = pathCoins;
                }
            }

            foreach (List<WaypointData> path in pathfinder.AllPaths)
            {
                if (!pathAnalyzer.HasTraps(path))
                {
                    CountCoins(path);

                    if (pathCoins == maxCoins && maxCoins != 0 && pathCoins != 0)
                    {
                        enemyMovement.PathToFollow = path;
                        maxCoins = 0;

                        pathAnalyzer.ColorPath(path, Color.green);

                        break;
                    }
                    else if(maxCoins == 0 && !pathAnalyzer.allPathsAreObserved)
                        pathAnalyzer.FindSafePath(pathfinder.AllPaths, enemyMovement);
                }
            }
        }
    }

    private void CountCoins(List<WaypointData> path)
    {
        pathCoins = 0;

        foreach (WaypointData waypoint in path)
        {
            if (waypoint.TextMesh.text != "")
                pathCoins += int.Parse(waypoint.TextMesh.text);
        }
    }

    private void PickUpCoin(WaypointData[] waypoints)
    {
        foreach (WaypointData waypoint in waypoints)
        {
            if (Mathf.Approximately(waypoint.transform.position.x, enemyMovement.transform.position.x) && Mathf.Approximately(waypoint.transform.position.z, enemyMovement.transform.position.z))
            {
                if (waypoint.TextMesh.text != "")
                {
                    pickedCoins += int.Parse(waypoint.TextMesh.text);
                    waypoint.TextMesh.text = "";
                }
            }
        }
    }

    public int RequiredCoins { get { return requiredCoins; } }
    public int PickedCoins { get { return pickedCoins; } }
}
