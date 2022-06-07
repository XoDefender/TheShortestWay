using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECoinCollector : MonoBehaviour
{
    private EWaypointData[] waypoints;
    private EPathfinder pathfinder;
    private EnemyMovement enemyMovement;
    private EStartTargetWaypoints startTargetWaypoints;
    private EPathAnalyzer pathAnalyzer;

    private int pathCoins = 0;
    private int maxCoins = 0;
    private int pickedCoins = 0;
    private int requiredCoins = 300;

    private void Awake()
    {
        waypoints = FindObjectsOfType<EWaypointData>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
        pathfinder = FindObjectOfType<EPathfinder>();
        startTargetWaypoints = FindObjectOfType<EStartTargetWaypoints>();
        pathAnalyzer = FindObjectOfType<EPathAnalyzer>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (pickedCoins <= requiredCoins)
            SelectTheGreatestPath();
        else
            startTargetWaypoints.ReadyToPickEndWaypoint = true;
        
        PickUpCoin(waypoints);
    }

    private void SelectTheGreatestPath()
    {
        if (pathfinder.AllPaths.Count == waypoints.Length && !enemyMovement.IsGoing)
        {
            foreach (List<EWaypointData> path in pathfinder.AllPaths)
            {
                if (!pathAnalyzer.HasTraps(path))
                {
                    CountCoins(path);

                    if (pathCoins > maxCoins)
                        maxCoins = pathCoins;
                }
            }

            foreach (List<EWaypointData> path in pathfinder.AllPaths)
            {
                if (!pathAnalyzer.HasTraps(path))
                {
                    CountCoins(path);

                    if (pathCoins == maxCoins && maxCoins != 0 && pathCoins != 0)
                    {
                        enemyMovement.PathToFollow = path;
                        maxCoins = 0;

                        ColorPath(path, Color.green);

                        break;
                    }
                    else if(maxCoins == 0 && !pathAnalyzer.allPathsAreObserved)
                        pathAnalyzer.FindSafePath(pathfinder.AllPaths, enemyMovement, ColorPath);
                }
            }
        }
    }

    private void CountCoins(List<EWaypointData> path)
    {
        pathCoins = 0;

        foreach (EWaypointData waypoint in path)
        {
            if (waypoint.TextMesh.text != "")
                pathCoins += int.Parse(waypoint.TextMesh.text);
        }
    }

    private void ColorPath(List<EWaypointData> path, Color color)
    {
        foreach (EWaypointData waypoint in path)
            waypoint.GetComponent<MeshRenderer>().material.color = color;
    }

    private void PickUpCoin(EWaypointData[] waypoints)
    {
        foreach (EWaypointData waypoint in waypoints)
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
