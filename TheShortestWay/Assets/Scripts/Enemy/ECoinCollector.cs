using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECoinCollector : MonoBehaviour
{
    private EWaypointData[] waypoints;
    private List<List<EWaypointData>> allPaths = new List<List<EWaypointData>>();
    private List<EWaypointData> finalPath = new List<EWaypointData>();

    private int maxCoins = 0;

    private void Awake()
    {
        waypoints = FindObjectsOfType<EWaypointData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (allPaths.Count == waypoints.Length)
        {
            foreach (List<EWaypointData> path in allPaths)
            {
                int coins = 0;

                foreach(EWaypointData waypoint in path)
                {
                    if(waypoint.TextMesh.text != "")
                        coins += int.Parse(waypoint.TextMesh.text);
                }

                if (coins > maxCoins)
                    maxCoins = coins;
            }

            foreach(List<EWaypointData> path in allPaths)
            {
                int coins = 0;

                foreach (EWaypointData waypoint in path)
                {
                    if (waypoint.TextMesh.text != "")
                        coins += int.Parse(waypoint.TextMesh.text);
                }

                if (coins == maxCoins)
                {
                    finalPath = path;

                    foreach (EWaypointData waypoint in finalPath)
                    {
                        waypoint.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                }
            }
        }
    }

    public List<List<EWaypointData>> AllPaths { get { return allPaths; } set { allPaths = value; } }
}
