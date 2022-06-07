using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPathAnalyzer : MonoBehaviour
{
    public List<EWaypointData> exploredTargetWaypoints = new List<EWaypointData>();

    private const string trapName = "Pf_Trap_Needle(Clone)";

    public bool allPathsAreObserved = false;

    public void FindSafePath(List<List<EWaypointData>> allPaths, EnemyMovement enemyMovement, Action<List<EWaypointData>, Color> ColorPath)
    {
        foreach (List<EWaypointData> pathToFollow in allPaths)
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

    public bool HasTraps(List<EWaypointData> path)
    {
        foreach (EWaypointData waypoint in path)
        {
            if (waypoint.transform.Find(trapName))
            {
                return true;
            }
        }

        return false;
    }
}
