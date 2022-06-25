using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPathAnalyzer : MonoBehaviour
{
    private List<WaypointData> exploredTargetWaypoints = new List<WaypointData>();

    private const string trapName = "Pf_Trap_Needle(Clone)";

    public bool allPathsAreObserved = false;

    public void FindSafePath(List<List<WaypointData>> allPaths, EnemyMovement enemyMovement)
    {
        foreach (List<WaypointData> pathToFollow in allPaths)
        {
            if (!HasTraps(pathToFollow))
            {
                if (pathToFollow.Count == 1)
                    continue;

                if (!exploredTargetWaypoints.Contains(pathToFollow[pathToFollow.Count - 1]))
                {
                    enemyMovement.PathToFollow = pathToFollow;
                    exploredTargetWaypoints.Add(pathToFollow[pathToFollow.Count - 1]);

                    ColorPath(pathToFollow, Color.red);

                    break;
                }
            }
        }

        allPathsAreObserved = true;
    }

    public void FindPathToTarget(List<List<WaypointData>> allPaths, WaypointData targetWaypoint, EnemyMovement enemyMovement)
    {
        foreach(List<WaypointData> path in allPaths)
        {
            if (path[path.Count - 1] == targetWaypoint && !HasTraps(path))
                enemyMovement.PathToFollow = path;
            else if(!allPathsAreObserved)
                FindSafePath(allPaths, enemyMovement);
        }
    }

    public void ColorPath(List<WaypointData> path, Color color)
    {
        foreach (WaypointData waypoint in path)
            waypoint.GetComponent<MeshRenderer>().material.color = color;
    }

    public bool HasTraps(List<WaypointData> path)
    {
        foreach (WaypointData waypoint in path)
        {
            if (waypoint.transform.Find(trapName))
            {
                return true;
            }
        }

        return false;
    }
}
